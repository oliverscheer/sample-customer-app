using Customer.DatabaseLogic;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public CountryController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _appDbContext.Countries.ToList();
            return Ok(result);
        }
    }
}
