using System;
using System.Threading;
using System.Threading.Tasks;
using Paymentsense.Coding.Challenge.Api.Cache;
using Paymentsense.Coding.Challenge.Api.RestCountries;
using Paymentsense.Coding.Challenge.Api.RestCountries.Response;

namespace Paymentsense.Coding.Challenge.Api.Repository
{
    public class CountryRepository : ICountryRepository
    {
        public const string CacheKey = "RestCountries";

        private readonly IResponseCacheService _responseCacheService;
        private readonly IRestCountriesClient _restCountriesClient;
        private readonly RestCountriesSettings _restCountriesSettings;

        public CountryRepository(IResponseCacheService responseCacheService, IRestCountriesClient restCountriesClient, RestCountriesSettings restCountriesSettings)
        {
            _responseCacheService = responseCacheService;
            _restCountriesClient = restCountriesClient;
            _restCountriesSettings = restCountriesSettings;
        }

        public async Task<Countries> GetCountries(CancellationToken cancellationToken)
        {
            var countries = await _responseCacheService.GetCachedResponse<Countries>(CacheKey, cancellationToken);

            if (countries == null)
            {
                countries = await _restCountriesClient.GetCountries(cancellationToken);
                await _responseCacheService.CacheResponseAsync(CacheKey, countries,
                    TimeSpan.FromMinutes(_restCountriesSettings.TimeToCacheInMinutes), cancellationToken);
            }

            return countries;
        }
    }
}
