using AuthenticApi.Services.UserService;
using System.Threading.Tasks;
using System.Web.Http;

namespace AuthenticApi.Controllers.Api
{
    public class UsersController : ApiController
    {
        private readonly IUserQueryService _userQueryService;

        public UsersController(IUserQueryService userQueryService)
        {
            _userQueryService = userQueryService;
        }

        public async Task<IHttpActionResult> Get(int id)
        {
            var result = await _userQueryService.GetAccessById(id);

            if ( result is null)
            {
                return BadRequest("Usuário não encontrado");
            }

            return Ok(result);
        }
    }
}
    