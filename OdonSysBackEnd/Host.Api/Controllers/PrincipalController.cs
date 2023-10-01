using Microsoft.AspNetCore.Mvc;

namespace Host.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrincipalController : ControllerBase
    {
        public PrincipalController()
        {

        }

        [HttpGet]
        public IActionResult Get()
        {
            var claims = User.Claims;
            return Ok(claims);
        }
    }
}
