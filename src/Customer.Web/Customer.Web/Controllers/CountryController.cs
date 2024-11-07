using Customer.BusinessLogic.Services.Country;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly CountryService _countryService;

        public CountryController(CountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _countryService.GetCountries();
            return Ok(result);
        }
    }
}
