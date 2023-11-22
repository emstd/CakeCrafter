using CakeCrafter.API.Controllers.Templates;
using CakeCrafter.Core.Interfaces.Services;
using CakeCrafter.Core.Models;

namespace CakeCrafter.API.Controllers
{
    public class IngredientsController : InformationController<Ingredient>
    {
        public IngredientsController(IGenericService<Ingredient> service) : base(service)
        {
        }
    }
}
