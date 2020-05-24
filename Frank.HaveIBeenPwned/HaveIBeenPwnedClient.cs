using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Frank.HaveIBeenPwned
{
    public class HaveIBeenPwnedClient : IAsyncDisposable
    {
        private readonly HaveIBeenPwnedConfiguration _haveIBeenPwnedConfiguration;
        private HttpClient _httpClient;
        private const string _basePath = "https://haveibeenpwned.com/api/v3";

        public HaveIBeenPwnedClient(HaveIBeenPwnedConfiguration haveIBeenPwnedConfiguration)
        {
            _haveIBeenPwnedConfiguration = haveIBeenPwnedConfiguration;

            var httpClient = new HttpClient();
            httpClient.BaseAddress = _haveIBeenPwnedConfiguration.BaseAddress;
            httpClient.DefaultRequestHeaders.Add("hibp-api-key", _haveIBeenPwnedConfiguration.ApiKey);
            httpClient.DefaultRequestHeaders.Add("user-agent", _haveIBeenPwnedConfiguration.ApplicationName);

            _httpClient = httpClient;
        }

        public async Task<bool> CheckPassword(string password, int threshold = 0)
        {
            var passwordDetails = await GetPasswordDetails(password);
            return passwordDetails.IsPwned && passwordDetails.TimesPwned > threshold;
        }

        public async Task<Password> GetPasswordDetails(string password)
        {
            var output = new Password()
            {
                PasswordString = password,
                Sha1Prefix = Hash.GetSha1(password).Substring(0, 5),
                Sha2Suffix = Hash.GetSha1(password).Substring(5),
                Sha1Hash = Hash.GetSha1(password)
            };

            using (var client = _httpClient)
            {
                var response = await client.GetStringAsync($"{_haveIBeenPwnedConfiguration.PwnedPasswordAddress}/{output.Sha1Prefix}");
                var result = response.Split('\n').Select(l => l.Split(':')).ToDictionary(l => l[0], l => Convert.ToInt32(l[1]));

                output.IsPwned = result.ContainsKey(output.Sha2Suffix);
                if (output.IsPwned)
                {
                    output.TimesPwned = result[output.Sha2Suffix];
                }
            }
            return output;
        }

        public async Task<bool> CheckForPastes(string username)
        {
            return await GetPastes(username) != null;
        }

        public async Task<IEnumerable<Paste>> GetPastes(string username)
        {
            using (var client = _httpClient)
            {
                var respose = await client.GetAsync($"{_basePath}/pasteaccount/{username}");

                try
                {
                    return JsonConvert.DeserializeObject<IEnumerable<Paste>>(respose.ReasonPhrase);
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Response string was not a list of pastes", e.Message);
                }
                try
                {
                    var paste = JsonConvert.DeserializeObject<Paste>(respose.ReasonPhrase);
                    return new List<Paste>() { paste };
                }
                catch (Exception e)
                {
                    //_logger.LogError(e, "Response string was not a paste");
                }

                return null;
            }
        }

        public async Task<bool> CheckSiteForBreaches(string website)
        {
            return await GetBreachesForSite(website) != null;
        }

        public async Task<IEnumerable<Breach>> GetBreachesForSite(string website)
        {
            using (var client = _httpClient)
            {
                var respose = await client.GetStringAsync($"{_basePath}/{Service.Breach.ToString().ToLowerInvariant()}/{website}");

                try
                {
                    return JsonConvert.DeserializeObject<IEnumerable<Breach>>(respose);
                }
                catch (Exception e)
                {
                    //_logger.LogError(e, "Response string was not a list of pastes");
                }
                try
                {
                    var paste = JsonConvert.DeserializeObject<Breach>(respose);
                    return new List<Breach>() { paste };
                }
                catch (Exception e)
                {
                    //_logger.LogError(e, "Response string was not a paste");
                }

                return null;
            }
        }

        public async Task<IEnumerable<Breach>> GetBreachesForAccount(string account)
        {
            using (var client = _httpClient)
            {
                var respose = await client.GetStringAsync($"{_basePath}/{Service.BreachedAccount.ToString().ToLowerInvariant()}/{account}?truncateResponse=false");

                try
                {
                    return JsonConvert.DeserializeObject<IEnumerable<Breach>>(respose);
                }
                catch (Exception e)
                {
                    //_logger.LogInformation(e, "Response string was not a list of pastes");
                }
                try
                {
                    var paste = JsonConvert.DeserializeObject<Breach>(respose);
                    return new List<Breach>() { paste };
                }
                catch (Exception e)
                {
                    //_logger.LogInformation(e, "Response string was not a paste");
                }

                return null;
            }
        }

        public async Task<IEnumerable<Breach>> GetAllBreaches()
        {
            using (var client = _httpClient)
            {
                var respose = await client.GetStringAsync($"{_basePath}/{Service.Breaches.ToString().ToLowerInvariant()}");

                try
                {
                    return JsonConvert.DeserializeObject<IEnumerable<Breach>>(respose);
                }
                catch (Exception e)
                {
                    //_logger.LogError(e, "Response string was not a list of breaches");
                }
                try
                {
                    var paste = JsonConvert.DeserializeObject<Breach>(respose);
                    return new List<Breach>() { paste };
                }
                catch (Exception e)
                {
                    //_logger.LogError(e, "Response string was not a breach");
                }

                return null;
            }
        }

        public async Task<IEnumerable<string>> GetDataClasses()
        {
            using (var client = _httpClient)
            {
                var respose = await client.GetStringAsync($"{_basePath}/{Service.DataClasses.ToString()}");

                try
                {
                    return JsonConvert.DeserializeObject<IEnumerable<string>>(respose);
                }
                catch (Exception e)
                {
                    //_logger.LogError(e, "Response string was not a list of breaches");
                }

                return null;
            }
        }

        public async ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }
    }
}