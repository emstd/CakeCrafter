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
            var user = User;

            return Ok(user.ToString());
        }
    }
}
