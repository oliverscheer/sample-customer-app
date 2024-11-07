using Customer.DatabaseLogic;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public CustomerController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _appDbContext.Customers.ToList();
            return Ok(result);
        }
    }
}
