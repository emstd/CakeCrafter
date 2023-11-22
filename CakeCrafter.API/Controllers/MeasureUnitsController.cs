﻿using CakeCrafter.API.Controllers.Templates;
using CakeCrafter.Core.Interfaces.Services;
using CakeCrafter.Core.Models;

namespace CakeCrafter.API.Controllers
{
    public class MeasureUnitsController : InformationController<MeasureUnit>
    {
        public MeasureUnitsController(IGenericService<MeasureUnit> service) : base(service)
        {
        }
    }
}
