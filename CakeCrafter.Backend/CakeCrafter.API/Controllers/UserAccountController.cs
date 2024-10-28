using CakeCrafter.API.Contracts;
using CakeCrafter.API.Options;
using CakeCrafter.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        public async Task<IActionResult> SignIn([FromBody] LoginRequest request, [FromServices] IConfiguration configuration)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid credentials {request}", request);
                return BadRequest(ModelState);
            }

            var user = await _userService.GetUserByEmail(request.Email);

            if (user == null)
            {
                _logger.LogError("This email {email} does not exist", request.Email);
                return BadRequest("This email does not exist");
            }

            if (request.Password != user.Password)
            {
                _logger.LogError("Incorrect password {password}", request.Password);
                return BadRequest("Incorret password");
            }

            var jwtOptions = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();

            if (jwtOptions == null)
            {
                throw new InvalidOperationException("JWT configuration not found");
            }

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(jwtOptions.TokenLifeTime)),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)), SecurityAlgorithms.HmacSha256)
                );

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            string tokenString = handler.WriteToken(token);

            return Ok(tokenString);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
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
