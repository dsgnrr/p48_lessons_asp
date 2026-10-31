using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace p48_lessons_asp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(new
            {
                Status=200,
                Message="Welcome to webapi"
            });
        }
    }
}
