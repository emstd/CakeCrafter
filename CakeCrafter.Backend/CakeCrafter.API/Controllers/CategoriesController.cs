using CakeCrafter.API.Controllers.Templates;
using CakeCrafter.Core.Interfaces.Services;
using CakeCrafter.Core.Models;

namespace CakeCrafter.API.Controllers
{
    public class CategoriesController : InformationController<Category>
    {
        public CategoriesController(IGenericService<Category> service) : base(service)
        {
        }
    }
}
