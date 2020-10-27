using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Paymentsense.Coding.Challenge.Api.Services;

namespace Paymentsense.Coding.Challenge.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly IRestCountriesService _restCountriesService;

        public CountriesController(IRestCountriesService restCountriesService)
        {
            _restCountriesService = restCountriesService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var response = await _restCountriesService.GetCountries(cancellationToken);
            return Ok(response);
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetCountry(string code, CancellationToken cancellationToken)
        {
            var country = await _restCountriesService.GetCountry(code, cancellationToken);
            if (country == null)
            {
                return NotFound();
            }

            return Ok(country);
        }
    }
}
