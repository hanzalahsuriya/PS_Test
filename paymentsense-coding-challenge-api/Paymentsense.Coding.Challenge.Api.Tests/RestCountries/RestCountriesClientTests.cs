using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Paymentsense.Coding.Challenge.Api.RestCountries;
using Paymentsense.Coding.Challenge.Api.RestCountries.Response;
using Paymentsense.Coding.Challenge.Api.Tests.Fakes;
using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests.RestCountries
{
    public class RestCountriesClientTests
    {
        private FakeHttpMessageHandler _httpHandler;
        private IRestCountriesClient _restCountriesClient;
        private string _sampleResponse;

        public RestCountriesClientTests()
        {
            _httpHandler = new FakeHttpMessageHandler();
            _restCountriesClient = new RestCountriesClient(new HttpClient(_httpHandler)
            {
                BaseAddress = new Uri("http://fake-rest-countries")
            });

            _sampleResponse = System.Text.Json.JsonSerializer.Serialize(new Countries()
            {
                new Country()
                {
                    Name = "C1"
                },

                new Country()
                {
                    Name = "C2"
                }
            });

            

        }

        [Fact]
        public async Task GetResponseWithValidUri()
        {
            // arrange
            _httpHandler.AddResponse(HttpMethod.Get, "http://fake-rest-countries/all", new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(_sampleResponse)
            });

            // act
            var response = await _restCountriesClient.GetCountries(default);

            // assert
            Assert.Equal(2, response.Count);
            Assert.Equal("C1", response.First().Name);
        }

        [Fact]
        public async Task GetResponseWithInvalidValidUri()
        {
            // arrange
            _httpHandler.AddResponse(HttpMethod.Get, "http://fake-rest-countries/invalid", new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(_sampleResponse)
            });

            await Assert.ThrowsAsync<NotImplementedException>(() => _restCountriesClient.GetCountries(default));

        }

    }
}
