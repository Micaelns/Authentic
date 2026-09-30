using AuthenticApi.Services.RoleService;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace AuthenticApi.Controllers.Api
{
    [RoutePrefix("api/v1/Role")]
    public class RoleController : ApiController
    {
        private readonly IRoleQueryService _roleQueryService;
        public RoleController(IRoleQueryService roleQueryService)
        {
            _roleQueryService = roleQueryService;
        }

        [HttpGet]
        [Route("user")]
        public async Task<IHttpActionResult> OfUser(int? softwareId = null)
        {
            var claimsPrincipal = User as ClaimsPrincipal;
            string nameIdentifier = claimsPrincipal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if ( int.TryParse(nameIdentifier, out int userId)) {
                var result = await _roleQueryService.GetSimpleRolesBySoftwareId(userId, softwareId??0);
                
                if (result.Count() == 0)
                {
                    return NotFound();
                }
                return Ok(result);
            }

            return Content(HttpStatusCode.BadRequest, "Usuário não identificado");
        }

        [HttpGet]
        [Route("software/{softwareId}")]
        public async Task<IHttpActionResult> Software(int softwareId)
        {
            var result = await _roleQueryService.GetRolesListPermissionBySoftwareId(softwareId);

            if (result.Count() == 0)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
