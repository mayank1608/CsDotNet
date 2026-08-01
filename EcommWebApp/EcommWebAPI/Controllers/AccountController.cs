using EcommWebAPI.JWTServices;
using EcommWebAPI.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtService _jwtService;

        public AccountController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<LogInResponseModel>> LogIn(LogInRequestModel request)
        {
            try
            {
                var result = await _jwtService.Authenticate(request);

                if (result is null)
                    return Unauthorized();

                return result;
            }
            catch
            {
                return Unauthorized();
            }
        }
    }
}
