using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using NDAccountManager.Core.Services;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NDAccountManager.API.Controllers
{
    public class UserController : CustomBaseController
    {
        //private readonly GraphServiceClient _client;
        //public UserController(GraphServiceClient client)
        //{
        //    _client = client;
        //}
        private readonly IUserService _userService;

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserInfo()
        {
            var user = HttpContext.User;
            var userId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found.");
            }
            var realUserObjectID = user.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier").Value;

            var userRole = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            var userName = user.Identity.Name;
            
            var ipAddress = user.Claims.FirstOrDefault(c => c.Type == "ipaddr").Value;

            // Kullanıcının gruplarını al
            //var memberOf = await _client.Users[userId].MemberOf.GetAsync();

            //var groupNames = new List<string>();

            //foreach (var directoryObject in memberOf.Value)
            //{
            //    if (directoryObject is Group group)
            //    {
            //        groupNames.Add(group.DisplayName);
            //    }
            //}
            return Ok(new
            {
                UserId = userId,
                UserName = userName,
                IpAddress = ipAddress,
                UserRole = userRole
            });
        }
    }
}
