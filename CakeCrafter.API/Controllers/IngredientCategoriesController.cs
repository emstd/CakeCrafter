﻿using CakeCrafter.API.Controllers.Templates;
using CakeCrafter.Core.Models;
using CakeCrafter.DataAccess;

namespace CakeCrafter.API.Controllers
{
    public class IngredientCategoriesController : InformationController<IngredientCategory>
    {
        public IngredientCategoriesController(CakeCrafterDbContext context) : base(context)
        {
        }
    }
}
