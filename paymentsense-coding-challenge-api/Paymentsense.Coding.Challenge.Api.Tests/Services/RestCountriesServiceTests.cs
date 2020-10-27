using System.Threading;
using System.Threading.Tasks;
using Moq;
using Paymentsense.Coding.Challenge.Api.Repository;
using Paymentsense.Coding.Challenge.Api.RestCountries.Response;
using Paymentsense.Coding.Challenge.Api.Services;
using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests.Services
{
    public class RestCountriesServiceTests
    {
        private readonly IRestCountriesService _restCountriesService;
        private readonly Mock<ICountryRepository> _countryRepository;

        public RestCountriesServiceTests()
        {
            _countryRepository = new Mock<ICountryRepository>();
            _restCountriesService = new RestCountriesService(_countryRepository.Object);

            _countryRepository.Setup(x => x.GetCountries(It.IsAny<CancellationToken>())).ReturnsAsync(new Countries()
            {
                new Country() {Name = "CNTRY1", Alpha3Code = "C1"},
                new Country() {Name = "CNTRY2", Alpha3Code = "C2"},
                new Country() {Name = "CNTRY3", Alpha3Code = "C3"},
                new Country() {Name = "CNTRY4", Alpha3Code = "C4"},
                new Country() {Name = "CNTRY5", Alpha3Code = "C5"},
                new Country() {Name = "CNTRY6", Alpha3Code = "C6"},
            }).Verifiable();
        }

        [Fact]
        public async Task WhenGetCountriesIsCalled_ExpectCountryResult()
        {
            // act
            var countries = await _restCountriesService.GetCountries(default);

            // assert
            Assert.NotNull(countries);
            Assert.Equal(6, countries.Count);
            _countryRepository.Verify(x => x.GetCountries(It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task WhenValidCodeIsPassed_ExpectCountryResult()
        {
            // act
            var country = await _restCountriesService.GetCountry("C1", default);

            // assert
            Assert.NotNull(country);
            Assert.Equal("CNTRY1", country.Name);

            _countryRepository.Verify(x=>x.GetCountries(It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task WhenValidCodeIsPassed_ExpectNullResult()
        {
            // act
            var country = await _restCountriesService.GetCountry("INVALID", default);


            // assert
            Assert.Null(country);
            _countryRepository.Verify(x => x.GetCountries(It.IsAny<CancellationToken>()));
        }

    }
}
