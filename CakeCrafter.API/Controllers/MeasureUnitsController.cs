using CakeCrafter.API.Controllers.Templates;
using CakeCrafter.Core.Models;
using CakeCrafter.DataAccess;

namespace CakeCrafter.API.Controllers
{
    public class MeasureUnitsController : InformationController<MeasureUnit>
    {
        public MeasureUnitsController(CakeCrafterDbContext context) : base(context)
        {
        }
    }
}
