using Paymentsense.Coding.Challenge.Api.RestCountries.Response;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Paymentsense.Coding.Challenge.Api.RestCountries.Exceptions;

namespace Paymentsense.Coding.Challenge.Api.RestCountries
{
    public class RestCountriesClient : IRestCountriesClient
    {
        private readonly HttpClient _httpClient;

        public RestCountriesClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Countries> GetCountries(CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync($"all", cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new RestCountriesApiException($"exception in rest countries");
            }

            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Countries>(json, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });
        }
    }
}
