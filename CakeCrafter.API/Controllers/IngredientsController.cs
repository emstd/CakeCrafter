using CakeCrafter.API.Controllers.Templates;
using CakeCrafter.API.Data;
using CakeCrafter.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace CakeCrafter.API.Controllers
{
    public class IngredientsController : InformationController<Ingredient>
    {
        public IngredientsController(AppDbContext context, DbSet<Ingredient> table) : base(context, table)
        {
        }
    }
}
