﻿using CakeCrafter.API.Controllers.Templates;
using CakeCrafter.Core.Interfaces.Services;
using CakeCrafter.Core.Models;

namespace CakeCrafter.API.Controllers
{
    public class TastesController : InformationController<Taste>
    {
        public TastesController(IGenericService<Taste> service) : base(service)
        {
        }
    }
}
