using CakeCrafter.API.Controllers.Templates;
using CakeCrafter.Core.Models;
using CakeCrafter.DataAccess;

namespace CakeCrafter.API.Controllers
{
    public class IngredientsController : InformationController<Ingredient>
    {
        public IngredientsController(CakeCrafterDbContext context) : base(context)
        {
        }
    }
}
