using AuthenticApi.DTOs.Auth;
using AuthenticApi.Exceptions;
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
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("logon")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> LogonAsync(LoginDTO loginDTO)
        {
            try
            {
                var result = await _authenticationService.LoginAsync(loginDTO);
                return Ok(result);
            }
            catch (InvalidCredentialsException ex)
            {
                return Content(HttpStatusCode.Unauthorized, new { ex.Message });
            }
            catch (Exception ex) when (
                                        ex is UserBlockedException ||
                                        ex is ForbiddenSoftwareAccessException
                                      )
            {
                return Content(HttpStatusCode.Forbidden, new { ex.Message });
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.Unauthorized, new { Message = "Erro interno" });
            }
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<IHttpActionResult> RefreshAsync(RefreshTokenDTO refreshTokenDTO)
        {
            try
            {
                var result = await _authenticationService.RefreshAsync(refreshTokenDTO.RefreshToken, refreshTokenDTO.DeviceId);
                return Ok(result);
            }
            catch (Exception ex) when (
                                        ex is NotFoundRefreshTokenException ||
                                        ex is DisabledRefreshTokenException ||
                                        ex is IncisiveRefreshTokenException
                                      )
            {
                return Content(HttpStatusCode.Forbidden, new { ex.Message });
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.BadRequest, new { Message = "Erro interno" });
            }
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IHttpActionResult> LogoutAsync(RefreshTokenDTO refreshTokenDTO)
        {
            try
            {
                var claimsPrincipal = User as ClaimsPrincipal;
                string nameIdentifier = claimsPrincipal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (nameIdentifier != null)
                {
                    await _authenticationService.LogoutAsync(refreshTokenDTO.RefreshToken, refreshTokenDTO.DeviceId);
                    return Content(HttpStatusCode.OK, new { Message = "Revogado tokens do usuário" });
                }
            }
            catch (Exception ex) when (
                                        ex is NotFoundRefreshTokenException ||
                                        ex is DisabledRefreshTokenException ||
                                        ex is IncisiveRefreshTokenException
                                      )
            {
                return Content(HttpStatusCode.Forbidden, new { ex.Message });
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.BadRequest, new { Message = "Erro interno" });
            }

            return Content(HttpStatusCode.BadRequest, "Usuário não identificado");
        }

    }
}
