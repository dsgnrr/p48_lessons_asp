using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using p48_lessons_asp.Services.Token;

namespace p48_lessons_asp.Controllers
{

    public class LoginDataModel
    {
        public string login { get; set; }
        public string password { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public UserController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDataModel model)
        {
            // todo: check user in db
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, model.login),
                new(ClaimTypes.Role, "Admin")
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GeenerateRefreshToken();

            return new ObjectResult(new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }
    }
}
