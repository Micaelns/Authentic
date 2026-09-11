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
        public async Task<IHttpActionResult> Logon(LoginDTO loginDTO)
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
            catch (UserBlockedException ex)
            {
                return Content(HttpStatusCode.Forbidden, new { ex.Message });
            }
            catch (ForbiddenSoftwareAccessException ex)
            {
                return Content(HttpStatusCode.Forbidden, new { ex.Message });
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.Unauthorized, new { Message = "Erro interno" } );
            }
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<IHttpActionResult> Refresh(RefreshTokenDTO refreshTokenDTO)
        {
            try
            {
                var result = await _authenticationService.RefreshAsync(refreshTokenDTO.RefreshToken, refreshTokenDTO.DeviceId);
                return Ok(result);
            }
            catch (NotFoundRefreshTokenException ex)
            {
                return Content(HttpStatusCode.Forbidden, new { ex.Message });
            }
            catch (DisabledRefreshTokenException ex)
            {
                return Content(HttpStatusCode.Forbidden, new { ex.Message });
            }
            catch (IncisiveRefreshTokenException ex)
            {
                return Content(HttpStatusCode.Forbidden, new { ex.Message });
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.BadRequest,new { Message = "Erro interno" } );
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
                    await _authenticationService.LogoutAsync(refreshTokenDTO.RefreshToken, refreshTokenDTO.DeviceId);
                    return Ok("Revogado tokens do usuário");
                }
            }
            catch (NotFoundRefreshTokenException ex)
            {
                return Content(HttpStatusCode.Forbidden, new { ex.Message });
            }
            catch (DisabledRefreshTokenException ex)
            {
                return Content(HttpStatusCode.Forbidden, new { ex.Message });
            }
            catch (IncisiveRefreshTokenException ex)
            {
                return Content(HttpStatusCode.Forbidden, new { ex.Message });
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.BadRequest, new { Message = "Erro interno" });
            }

            return Content(HttpStatusCode.BadRequest,"Usuário não identificado");
        }

    }
}
