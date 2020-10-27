using System.Threading;
using System.Threading.Tasks;
using Paymentsense.Coding.Challenge.Api.RestCountries.Response;

namespace Paymentsense.Coding.Challenge.Api.Repository
{
    public interface ICountryRepository
    {
        Task<Countries> GetCountries(CancellationToken cancellationToken);
    }
}
