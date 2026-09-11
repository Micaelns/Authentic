using AuthenticApi.DTOs.Auth;
using AuthenticApi.DTOs.Users;
using AuthenticApi.Services.AuthService;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace AuthenticApi.Controllers.Api
{
    [RoutePrefix("api/v1/Auth")]
    public class AuthController : ApiController
    {
        private readonly IAuthQueryService _authQueryService;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IRefreshTokenService _refreshTokenService;

        public AuthController(IAuthQueryService authQueryService, IJwtTokenService jwtTokenService, IRefreshTokenService refreshTokenService)
        {
            _authQueryService = authQueryService;
            _jwtTokenService = jwtTokenService;
            _refreshTokenService = refreshTokenService;
        }

        [HttpPost]
        [Route("logon")]
        public async Task<IHttpActionResult> Logon(LoginDTO loginDTO)
        {
            try
            {
                var user = await _authQueryService.Logon(loginDTO);
                var token = _jwtTokenService.GenerateToken(user);
                var refreshToken = await _refreshTokenService.Generate(user, "external_app");

                return Ok(new TokenDTO
                {
                    Token = token,
                    RefreshToken = refreshToken.TokenHash
                });

            }
            catch (Exception ex)
            {
                return Content(
                                HttpStatusCode.Unauthorized,
                                new
                                {
                                    ex.Message
                                }
                            );
            }
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<IHttpActionResult> Refresh(RefreshTokenDTO refreshTokenDTO)
        {
            try
            {
                var result = await _refreshTokenService.RefreshAsync(
                refreshTokenDTO.RefreshToken,
                refreshTokenDTO.DeviceId);

                var userLoged = new UserLogedDTO { 
                    Id = result.User.Id,
                    Email = result.User.Email,
                    Name = result.User.Name,
                    NickName = result.User.NickName
                };
                var token = _jwtTokenService.GenerateToken(userLoged);

                return Ok(new TokenDTO
                {
                    Token = token,
                    RefreshToken = result.TokenHash
                });
            }
            catch (Exception ex)
            {
                return Content(
                                HttpStatusCode.BadRequest,
                                new
                                {
                                    ex.Message
                                }
                            );
            }
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IHttpActionResult> Logout(RefreshTokenDTO refreshTokenDTO)
        {
            try
            {
                var claimsPrincipal = User as ClaimsPrincipal;
                string nameIdentifier = claimsPrincipal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (int.TryParse(nameIdentifier, out int userId))
                {
                    await _refreshTokenService.RevokeTokenAsync(refreshTokenDTO.RefreshToken, refreshTokenDTO.DeviceId);
                    return Ok("Revogado tokens do usuário");
                }
            }
            catch (Exception ex)
            {
                return Content(
                                HttpStatusCode.BadRequest,
                                new
                                {
                                    ex.Message
                                }
                            );
            }
            return Content(
                                HttpStatusCode.BadRequest, 
                                "Usuário não identificado"
                          );
        }

    }
}
