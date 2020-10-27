using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Paymentsense.Coding.Challenge.Api.Cache;
using Paymentsense.Coding.Challenge.Api.Repository;
using Paymentsense.Coding.Challenge.Api.RestCountries;
using Paymentsense.Coding.Challenge.Api.RestCountries.Response;
using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests.Repository
{
    public class CountryRepositoryTests
    {
        private readonly ICountryRepository _countryRepository;
        private readonly Mock<IResponseCacheService> _responseCacheService;
        private readonly Mock<IRestCountriesClient> _restCountriesClient;
        private readonly Countries _countries;

        public CountryRepositoryTests()
        {
            _countries = new Countries() {new Country() {Name = "C1"}, new Country() {Name = "C2"}};

            _responseCacheService = new Mock<IResponseCacheService>();
            _restCountriesClient = new Mock<IRestCountriesClient>();

            _countryRepository = new CountryRepository(_responseCacheService.Object, _restCountriesClient.Object,
                new RestCountriesSettings()
                {
                    APIURL = "http://restcountries",
                    TimeToCacheInMinutes = 1
                });

            _restCountriesClient.Setup(x => x.GetCountries(default)).ReturnsAsync(_countries).Verifiable();

            _responseCacheService.Setup(x => x.CacheResponseAsync<It.IsAnyType>(It.IsAny<string>(),
                It.IsAny<It.IsAnyType>(), It.IsAny<TimeSpan>(), It.IsAny<CancellationToken>())).Verifiable();
        }

        [Fact]
        public async Task WhenCacheIsNotAvailable_CallHttpClient_ExpectResponseToBeCached()
        {
            // arrange
            _responseCacheService
                .Setup(x => x.GetCachedResponse<Countries>(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Countries) (null))
                .Verifiable();

            // act
            var response = await _countryRepository.GetCountries(default);

            // assert
            _restCountriesClient.Verify(x=>x.GetCountries(It.IsAny<CancellationToken>()));

            _responseCacheService.Verify(x => x.CacheResponseAsync(CountryRepository.CacheKey, It.IsAny<It.IsAnyType>(),
                TimeSpan.FromMinutes(1), It.IsAny<CancellationToken>()));

            _responseCacheService.Verify(x =>
                x.GetCachedResponse<Countries>(CountryRepository.CacheKey, It.IsAny<CancellationToken>()));

            Assert.NotNull(response);
            Assert.Equal(2, response.Count);
            Assert.Equal("C1", response.First().Name);
        }

        [Fact]
        public async Task WhenCacheIsAvailable_ExpectHttpClientNotCalled()
        {
            // arrange
            _responseCacheService
                .Setup(x => x.GetCachedResponse<Countries>(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_countries)
                .Verifiable();

            // act
            var response = await _countryRepository.GetCountries(default);

            // assert
            _restCountriesClient.Verify(x => x.GetCountries(It.IsAny<CancellationToken>()), Times.Never);

            _responseCacheService.Verify(x => x.CacheResponseAsync(CountryRepository.CacheKey, It.IsAny<It.IsAnyType>(),
                TimeSpan.FromMinutes(1), It.IsAny<CancellationToken>()), Times.Never);

            _responseCacheService.Verify(x =>
                x.GetCachedResponse<Countries>(CountryRepository.CacheKey, It.IsAny<CancellationToken>()));
            Assert.NotNull(response);
            Assert.Equal(2, response.Count);
            Assert.Equal("C1", response.First().Name);
        }

    }
}
