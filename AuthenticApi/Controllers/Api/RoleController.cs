using AuthenticApi.Services.PermissionService;
using AuthenticApi.Services.RoleService;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

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

        [Route("user/{userId}")]
        [HttpGet]
        public async Task<IHttpActionResult> OfUser(int userId, int softwareId)
        {
            var result = await _roleQueryService.GetSimpleRolesBySoftwareId(userId, softwareId);

            if (result.Count() == 0)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
