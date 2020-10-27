using System.Threading;
using System.Threading.Tasks;
using Paymentsense.Coding.Challenge.Api.RestCountries.Response;

namespace Paymentsense.Coding.Challenge.Api.RestCountries
{
    public interface IRestCountriesClient
    {
        public Task<Countries> GetCountries(CancellationToken cancellationToken);
    }
}
