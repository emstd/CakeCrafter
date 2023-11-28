using CakeCrafter.API.Controllers.Templates;
using CakeCrafter.Core.Interfaces.Services;
using CakeCrafter.Core.Models;

namespace CakeCrafter.API.Controllers
{
    public class IngredientCategoriesController : InformationController<IngredientCategory>
    {
        public IngredientCategoriesController(IGenericService<IngredientCategory> service) : base(service)
        {
        }
    }
}
