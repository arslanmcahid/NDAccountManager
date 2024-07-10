using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;

namespace NDAccountManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize()]
    public class UserController : ControllerBase
    {
        private readonly GraphServiceClient _client;
        private readonly ITokenAcquisition _token;

        public UserController(GraphServiceClient client, ITokenAcquisition token)
        {
            _client = client;
            _token = token;
        }

        [RequiredScope("Forecast.Read")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var user = HttpContext.User;
            var userId = user.Claims.FirstOrDefault(c => c.Type == "oid")?.Value;
            var userName = user.Identity.Name;
            var userGroupName = user.Identity.AuthenticationType;

            //var userGroups = await _client.Users[userId].MemberOf.Request().GetAsync();
            //var groupNames = userGroups.OfType<Group>().Select(g => g.DisplayName).ToList();
            return Ok(new { UserId = userId, UserName= userName });
        }
    }
}
