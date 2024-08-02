using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NDAccountManager.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace NDAccountManager.API.Controllers
{

    public class AuthController : CustomBaseController
    {
        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpGet("signin")]
        public IActionResult SignIn()
        {
            var redirectUrl = Url.Action(nameof(SignedIn), "Auth");
            return Challenge(new AuthenticationProperties { RedirectUri = redirectUrl }, OpenIdConnectDefaults.AuthenticationScheme);
        }
        [HttpGet("signedin")]
        [Authorize]
        public IActionResult SignedIn()
        {
            return Ok("User signed in successfully.");
        }
        [HttpGet("signout")]
        public IActionResult SignOut()
        {
            var callbackUrl = Url.Action(nameof(SignedOut), "Auth", values: null, protocol: Request.Scheme);
            return SignOut(new AuthenticationProperties { RedirectUri = callbackUrl }, CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme);
        }
        [HttpGet("signedout")]
        public IActionResult SignedOut()
        {
            return Ok("User signed out successfully.");
        }





        [HttpPost("get-token")]
        public async Task<IActionResult> GetToken()
        {
            string[] scopes = new string[] { "https://graph.microsoft.com/.default" };
            var token = await _tokenService.GetAccessTokenAsync(scopes);
            return Ok(new {accessToken = token});
        }
    }
}
