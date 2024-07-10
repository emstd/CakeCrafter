using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
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
            return await AuthenticateAsync();
        }
    }
}
