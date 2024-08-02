using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using NDAccountManager.Core.DTOs;
using NDAccountManager.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDAccountManager.Service.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfidentialClientApplication _app;

        public TokenService(IOptions<AzureADTokenOptionsDto> azureADOptions)
        {
            var options = azureADOptions.Value;
            _app = ConfidentialClientApplicationBuilder.Create(options.ClientId)
                    .WithClientSecret(options.ClientSecret)
                    .WithAuthority(new Uri($"https://login.microsoftonline.com/{options.TenantId}"))
                    .Build();
        }

        public async Task<string> GetAccessTokenAsync(string[] scopes)
        {
            var result = await _app.AcquireTokenForClient(scopes).ExecuteAsync();
            return result.AccessToken;
        }
    }
}
