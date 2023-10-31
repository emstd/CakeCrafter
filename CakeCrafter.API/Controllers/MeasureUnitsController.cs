using CakeCrafter.API.Controllers.Templates;
using CakeCrafter.API.Data;
using CakeCrafter.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CakeCrafter.API.Controllers
{
    public class MeasureUnitsController : InformationController<MeasureUnit>
    {
        public MeasureUnitsController(AppDbContext context) : base(context)
        {
        }
    }
}
