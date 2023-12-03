using CakeCrafter.API.Controllers.Templates;
using CakeCrafter.Core.Interfaces.Services;
using CakeCrafter.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CakeCrafter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CakeIngredientsController : InformationController<CakesIngredients>
    {
        public CakeIngredientsController(IGenericService<CakesIngredients> service) : base(service)
        {
        }
    }
}
