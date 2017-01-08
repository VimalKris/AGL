using AGL.DEV.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AGL.DEV.Repository
{
    public class WebServiceRepository : IRepository
    {
        private string _webServiceUrl;
        private HttpClient _httpClient;

        public WebServiceRepository(string webServiceUrl, HttpClient httpClient)
        {
            if (webServiceUrl == null)
                throw new ArgumentNullException("webServiceUrl");

            if (httpClient == null)
                throw new ArgumentNullException("httpClient");

            _webServiceUrl = webServiceUrl;

            if(_httpClient == null)
                _httpClient = httpClient;
        }

        public async Task<List<Person>> GetPeopleData()
        {
            string jsonData = await GetPeopleDataAsync();

            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Converters = new[] { new StringEnumConverter() }
            };

            return JsonConvert.DeserializeObject<List<Person>>(jsonData, jsonSettings);
        }

        private async Task<string> GetPeopleDataAsync()
        {
            if (_webServiceUrl == null)
                throw new Exception("Web Service Url not specified");

            if (_httpClient == null)
                throw new Exception("HttpClient instance not set");

            HttpResponseMessage response = await _httpClient.GetAsync(_webServiceUrl);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();        
        }
    }
}
