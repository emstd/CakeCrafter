using CakeCrafter.API.Contracts;
using CakeCrafter.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CakeCrafter.API.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class UserAccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserAccountController> _logger;
        public UserAccountController(IUserService service, ILogger<UserAccountController> logger)
        {
            _userService = service;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] LoginRequest request)
        {
            if (ModelState.IsValid)
            {
                _logger.LogError("Invalid credentials {request}", request);
                return BadRequest(ModelState);
            }

            var user = await _userService.GetUserByEmail(request.Email);

            if (user == null)
            {
                return BadRequest("This email does not exist");
            }

            if (request.Password != user.Password)
            {
                return BadRequest("Incorret password");
            }

            //создать access token и refresh token

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
