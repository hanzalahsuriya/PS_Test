using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Paymentsense.Coding.Challenge.Api.Controllers;
using Paymentsense.Coding.Challenge.Api.Response;
using Paymentsense.Coding.Challenge.Api.RestCountries.Response;
using Paymentsense.Coding.Challenge.Api.Services;
using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests.Controllers
{
    public class PaymentsenseCodingChallengeControllerTests
    {
        private readonly Mock<IRestCountriesService> _mockRestCountries;
        private readonly CountriesController _countriesController;

        public PaymentsenseCodingChallengeControllerTests()
        {
            _mockRestCountries = new Mock<IRestCountriesService>();
            _countriesController = new CountriesController(_mockRestCountries.Object);
        }

        [Fact]
        public async Task WhenGetCountries_ExpectListOfCountriesWithOkResponse()
        {

            // arrange
            _mockRestCountries.Setup((x => x.GetCountries(It.IsAny<CancellationToken>()))).ReturnsAsync(
                new List<CountryShortResponse>()
                {
                    new CountryShortResponse()
                    {
                        Name = "XYZ"
                    }
                }).Verifiable();

            // act
            var result = await _countriesController.Get(default);
            
            // assert
            var objectResult = result as OkObjectResult;

            Assert.NotNull(objectResult);
            var countries = objectResult.Value as List<CountryShortResponse>;

            Assert.Equal(objectResult.StatusCode, (int)HttpStatusCode.OK);
            Assert.NotNull(countries);
            Assert.Single(countries);
            Assert.Equal("XYZ", countries.First().Name);
            _mockRestCountries.Verify(x => x.GetCountries(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task WhenGetCountryByCountryCode_ExpectCountryDetailsWithOkResponse()
        {
            // arrange
            _mockRestCountries.Setup((x => x.GetCountry(It.IsAny<string>(), It.IsAny<CancellationToken>()))).ReturnsAsync(
                new Country()
                {
                    Alpha3Code = "AA",
                    Name  = "AAAA"
                }).Verifiable();

            // act
            var result = await _countriesController.GetCountry("AA", default);

            // assert
            var objectResult = result as OkObjectResult;

            Assert.NotNull(objectResult);
            var country = objectResult.Value as Country;

            Assert.Equal(objectResult.StatusCode, (int)HttpStatusCode.OK);
            Assert.NotNull(country);
            Assert.Equal("AA", country.Alpha3Code);
            _mockRestCountries.Verify(x=>x.GetCountry(It.Is<string>(x=> x== "AA"), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task WhenGetCountryByCountryCodeDoesNtExists_ExpectNotFoundResponse()
        {
            // arrange
            _mockRestCountries.Setup((x => x.GetCountry(It.IsAny<string>(), It.IsAny<CancellationToken>())))
                .ReturnsAsync((Country) null).Verifiable();

            // act
            var result = await _countriesController.GetCountry("AA", default);

            // assert
            var notFoundResult = result as NotFoundResult;

            Assert.NotNull(notFoundResult);

            Assert.Equal(notFoundResult.StatusCode, (int)HttpStatusCode.NotFound);
            _mockRestCountries.Verify(x => x.GetCountry(It.Is<string>(x => x == "AA"), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
