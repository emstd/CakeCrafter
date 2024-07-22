using CakeCrafter.API.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CakeCrafter.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersAccountController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(Task.CompletedTask);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userNameClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);

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


            return Ok
                (new
                {
                    //claimType,
                    //userName,
                    User.Identity,
                    User.Identity.AuthenticationType
                });
        }
    }
}
