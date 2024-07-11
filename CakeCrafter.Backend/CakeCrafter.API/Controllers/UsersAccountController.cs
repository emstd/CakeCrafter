using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CakeCrafter.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersAccountController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userNameClaim = User.Claims.FirstOrDefault(x => x.Type == "UserName");

            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                return Forbid();
            }

            if (userNameClaim is null)
            {
                return Forbid();
            }

            var claimType = userNameClaim.Type;
            var userName = userNameClaim.Value;

            return Ok(new { claimType, userName });
        }
    }
}
