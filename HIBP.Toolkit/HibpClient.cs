using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace HIBP.Toolkit
{
    public class HibpClient : IHibpClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<HibpClient> _logger;
        private readonly HibpConfiguration _options;

        private const string _basePath = "https://haveibeenpwned.com/api/v3";

        public HibpClient(IHttpClientFactory clientFactory, ILogger<HibpClient> logger, IOptions<HibpConfiguration> options)
        {
            _clientFactory = clientFactory;
            _logger = logger;
            _options = options.Value;
        }

        private HttpClient HttpClient()
        {
            var output = _clientFactory.CreateClient();
            output.BaseAddress = _options.BaseAddress;
            output.DefaultRequestHeaders.Add("hibp-api-key", _options.ApiKey);
            output.DefaultRequestHeaders.Add("user-agent", _options.ApplicationName);
            return output;
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

            using (var client = HttpClient())
            {
                var response = await client.GetStringAsync($"{_options.PwnedPasswordAddress}/{output.Sha1Prefix}");
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
            using (var client = HttpClient())
            {
                var respose = await client.GetStringAsync($"{_basePath}/pasteaccount/{username}");

                try
                {
                    return JsonConvert.DeserializeObject<IEnumerable<Paste>>(respose);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Response string was not a list of pastes");
                }
                try
                {
                    var paste = JsonConvert.DeserializeObject<Paste>(respose);
                    return new List<Paste>() { paste };
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Response string was not a paste");
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
            using (var client = HttpClient())
            {
                var respose = await client.GetStringAsync($"{_basePath}/{Service.Breach.ToString().ToLowerInvariant()}/{website}");

                try
                {
                    return JsonConvert.DeserializeObject<IEnumerable<Breach>>(respose);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Response string was not a list of pastes");
                }
                try
                {
                    var paste = JsonConvert.DeserializeObject<Breach>(respose);
                    return new List<Breach>() { paste };
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Response string was not a paste");
                }

                return null;
            }
        }

        public async Task<IEnumerable<Breach>> GetBreachesForAccount(string account)
        {
            using (var client = HttpClient())
            {
                var respose = await client.GetStringAsync($"{_basePath}/{Service.BreachedAccount.ToString().ToLowerInvariant()}/{account}?truncateResponse=false");

                try
                {
                    return JsonConvert.DeserializeObject<IEnumerable<Breach>>(respose);
                }
                catch (Exception e)
                {
                    _logger.LogInformation(e, "Response string was not a list of pastes");
                }
                try
                {
                    var paste = JsonConvert.DeserializeObject<Breach>(respose);
                    return new List<Breach>() { paste };
                }
                catch (Exception e)
                {
                    _logger.LogInformation(e, "Response string was not a paste");
                }

                return null;
            }
        }

        public async Task<IEnumerable<Breach>> GetAllBreaches()
        {
            using (var client = HttpClient())
            {
                var respose = await client.GetStringAsync($"{_basePath}/{Service.Breaches.ToString().ToLowerInvariant()}");

                try
                {
                    return JsonConvert.DeserializeObject<IEnumerable<Breach>>(respose);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Response string was not a list of breaches");
                }
                try
                {
                    var paste = JsonConvert.DeserializeObject<Breach>(respose);
                    return new List<Breach>() { paste };
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Response string was not a breach");
                }

                return null;
            }
        }

        public async Task<IEnumerable<string>> GetDataClasses()
        {
            using (var client = HttpClient())
            {
                var respose = await client.GetStringAsync($"{_basePath}/{Service.DataClasses.ToString()}");

                try
                {
                    return JsonConvert.DeserializeObject<IEnumerable<string>>(respose);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Response string was not a list of breaches");
                }

                return null;
            }
        }
    }
}