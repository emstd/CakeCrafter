using CakeCrafter.API.Controllers.Templates;
using CakeCrafter.Core.Models;
using CakeCrafter.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace CakeCrafter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CakeIngredientsController : InformationController<CakesIngredients>
    {
        public CakeIngredientsController(CakeCrafterDbContext context) : base(context) { }
    }
}
