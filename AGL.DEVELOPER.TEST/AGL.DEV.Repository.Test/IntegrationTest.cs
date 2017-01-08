using AGL.DEV.Model;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AGL.DEV.Repository.Test
{
    [TestFixture]
    public class IntegrationTest
    {
        private const string _webServiceUrl = "http://agl-developer-test.azurewebsites.net/people.json";
        private static HttpClient _httpClient = new HttpClient();

        [Test]
        // Test if the http request has the right web service URL
        public void WebService_Url_Should_Be_Valid()
        {
            // Act
            var response = _httpClient.GetAsync(_webServiceUrl).Result;

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        // Test if the http request is using the right method to access the web service
        public void WebService_HttpMethod_Should_Be_Get()
        {
            // Act
            var response = _httpClient.GetAsync(_webServiceUrl).Result;
            
            // Assert
            Assert.That(response.RequestMessage.Method, Is.EqualTo(HttpMethod.Get));
        }

        [Test]
        // Test if the response content type is JSON (so that the response can be deserialized with a json deserializer)
        public void WebService_ContentType_Should_Be_Json()
        {
            // Act
            var response = _httpClient.GetAsync(_webServiceUrl).Result;
            
            // Assert
            Assert.That(response.Content.Headers.ContentType.MediaType, Is.EqualTo("application/json"));
        }

        [Test]
        public async Task Repository_Returns_Valid_Data()
        {
            // Act
            IRepository repository = new WebServiceRepository(_webServiceUrl, _httpClient);
            List<Person> personData = await repository.GetPeopleData();
            
            // Assert
            Assert.That(personData.GetType(), Is.EqualTo(typeof(List<Person>)));
        }
    }
}
