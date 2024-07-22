using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace CakeCrafter.API
{
    public class AppAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public AppAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
                                ILoggerFactory logger,
                                UrlEncoder encoder,
                                ISystemClock clock) : base(options, logger, encoder, clock)
        {

        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var claims = new[]
            {
                new Claim("userId", "12345"),
                new Claim(ClaimTypes.Name, "TestUser")
            };

            var claimsIdentity = new ClaimsIdentity(claims, "MyScheme");

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            Context.User = claimsPrincipal;

            var authenticationTicket = new AuthenticationTicket(claimsPrincipal, "MyScheme");
            
            return AuthenticateResult.Success(authenticationTicket);
        }
    }
}
