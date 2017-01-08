using AGL.DEV.Model;
using Moq;
using NUnit.Framework;
using RichardSzalay.MockHttp;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AGL.DEV.Repository.Test
{
    [TestFixture]
    public class UnitTest
    {
        private string _webServiceJson;

        [OneTimeSetUp]
        public void Initialize()
        {
            _webServiceJson = @"[{""name"":""Alice"",""gender"":""Female"",""age"":64,""pets"":[{""name"":""Simba"",""type"":""Cat""},
                                    {""name"":""Nemo"",""type"":""Fish""},{""name"":""Jim"",""type"":""Cat""}]}, 
                                 {""name"":""Bob"",""gender"":""Male"",""age"":23,""pets"":[{""name"":""Garfield"",""type"":""Cat""},
                                    {""name"":""Fido"",""type"":""Dog""},{""name"":""Tom"",""type"":""Cat""}]},
                                 {""name"":""Steve"",""gender"":""Male"",""age"":45,""pets"":null}]";
        }

        [Test]
        public async Task Repository_Returns_Valid_Data()
        {
            // Arrange
            MockHttpMessageHandler mockHttpMessageHandler = new MockHttpMessageHandler();
            mockHttpMessageHandler.When(HttpMethod.Get, "/test").Respond(HttpStatusCode.OK, "application/json", _webServiceJson);
            Mock<HttpClient> mockHttpClient = new Mock<HttpClient>(mockHttpMessageHandler);
            
            // Act
            IRepository repository = new WebServiceRepository("http://invalid/test", mockHttpClient.Object);
            List<Person> personData = await repository.GetPeopleData();
            
            // Assert
            Assert.That(personData.GetType(), Is.EqualTo(typeof(List<Person>)));
        }

        [Test]
        public void Repository_Returns_Exception_When_WebService_Url_Is_Not_Found()
        {
            // Arrange
            MockHttpMessageHandler mockHttpMessageHandler = new MockHttpMessageHandler();
            mockHttpMessageHandler.When(HttpMethod.Get, "/test").Respond(HttpStatusCode.NotFound, "application/json", _webServiceJson);
            Mock<HttpClient> mockHttpClient = new Mock<HttpClient>(mockHttpMessageHandler);
            
            // Act
            IRepository repository = new WebServiceRepository("http://invalid/test", mockHttpClient.Object);
            HttpRequestException httpRequestException = Assert.ThrowsAsync<HttpRequestException>(async () => await repository.GetPeopleData());
            
            // Assert
            Assert.That(httpRequestException.Message, Is.EqualTo("Response status code does not indicate success: 404 (Not Found)."));
        }
    }
}
