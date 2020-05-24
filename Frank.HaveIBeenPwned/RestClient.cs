using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Frank.HaveIBeenPwned
{
    public class RestClient : IDisposable
    {
        private HttpClient _httpClient;
        private readonly Uri _address;
        private HttpRequestMessage _requestMessage;

        public RestClient(string address, params KeyValuePair<string, string>[]? defaultRequestHeaders)
        {
            _address = new Uri(address);
            var httpClient = new HttpClient();
            httpClient.BaseAddress = _address;
            foreach (var (key, value) in defaultRequestHeaders)
            {
                httpClient.DefaultRequestHeaders.Add(key, value);
            }
            _httpClient = httpClient;
            //httpClient.DefaultRequestHeaders.Add("hibp-api-key", _haveIBeenPwnedConfiguration.ApiKey);
            //httpClient.DefaultRequestHeaders.Add("user-agent", _haveIBeenPwnedConfiguration.ApplicationName);
        }

        //public async Task<T> PostAsync<T>(T value) where T : class, new()
        //{
        //    var json = JsonSerializer.T
        //    _requestMessage.Content = new StringContent();
        //    var response = _httpClient.SendAsync()
        //        Guid.TryParse("", out var newGuid)
        //}

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}