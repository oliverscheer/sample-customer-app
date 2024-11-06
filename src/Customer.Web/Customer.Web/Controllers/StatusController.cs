using Microsoft.AspNetCore.Mvc;

namespace Customer.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Status: OK");
        }
    }
}
