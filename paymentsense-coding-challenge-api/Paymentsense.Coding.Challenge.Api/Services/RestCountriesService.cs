using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Paymentsense.Coding.Challenge.Api.Repository;
using Paymentsense.Coding.Challenge.Api.Response;
using Paymentsense.Coding.Challenge.Api.RestCountries.Response;

namespace Paymentsense.Coding.Challenge.Api.Services
{
    public class RestCountriesService : IRestCountriesService
    {
        private readonly ICountryRepository _countryRepository;

        public RestCountriesService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<List<CountryShortResponse>> GetCountries(CancellationToken cancellationToken)
        {
            var countries = await _countryRepository.GetCountries(cancellationToken);

            return countries.Select(c => new CountryShortResponse
            {
                Code = c.Alpha3Code,
                Name = c.Name,
                Flag = c.Flag
            }).ToList();
        }

        public async Task<Country> GetCountry(string code, CancellationToken cancellationToken)
        {
            return (await _countryRepository.GetCountries(cancellationToken)).FirstOrDefault(c => c.Alpha3Code == code);
        }
    }
}
