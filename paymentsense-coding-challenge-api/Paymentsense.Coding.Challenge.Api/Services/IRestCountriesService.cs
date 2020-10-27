using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Paymentsense.Coding.Challenge.Api.Response;
using Paymentsense.Coding.Challenge.Api.RestCountries.Response;

namespace Paymentsense.Coding.Challenge.Api.Services
{
    public interface IRestCountriesService
    {
        Task<List<CountryShortResponse>> GetCountries(CancellationToken cancellationToken);

        Task<Country> GetCountry(string code, CancellationToken cancellationToken);
    }
}
