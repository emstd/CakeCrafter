using CakeCrafter.API.Controllers.Templates;
using CakeCrafter.Core.Models;
using CakeCrafter.DataAccess;

namespace CakeCrafter.API.Controllers
{
    public class TastesController : InformationController<Taste>
    {
        public TastesController(CakeCrafterDbContext context) : base(context)
        {
        }
    }
}
