using CakeCrafter.API.Controllers.Templates;
using CakeCrafter.Core.Models;
using CakeCrafter.DataAccess;

namespace CakeCrafter.API.Controllers
{
    public class CategoriesController : InformationController<Category>
    {
        public CategoriesController(CakeCrafterDbContext context) : base(context) { }
    }
}
