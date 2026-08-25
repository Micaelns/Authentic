using AuthenticApi.DTOs.Auth;
using AuthenticApi.Services.AuthService;
using System.Threading.Tasks;
using System.Web.Http;

namespace AuthenticApi.Controllers.Api
{
    [RoutePrefix("api/v1/Auth")]
    public class AuthController : ApiController
    {
        private readonly IAuthQueryService _authQueryService; 
        private readonly IJwtTokenService _jwtTokenService;

        public AuthController(IAuthQueryService authQueryService,  IJwtTokenService jwtTokenService)
        {
            _authQueryService = authQueryService; 
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost]
        [Route("logon")]
        public async Task<IHttpActionResult> Logon(LoginDTO loginDTO)
        {
            try
            {
                var user = await _authQueryService.Logon(loginDTO);
                var token = _jwtTokenService.GenerateToken(user);

                return Ok(new
                {
                    Token = token
                });

            } catch 
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        [Route("logout")]
        public IHttpActionResult Logout()
        {
            var temp = "micael Nunes - logout";
            return Ok(temp);
        }

    }
}
