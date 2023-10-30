using CakeCrafter.API.Controllers.Templates;
using CakeCrafter.API.Data;
using CakeCrafter.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CakeCrafter.API.Controllers
{
    public class TastesController : InformationController<Taste>
    {
        public TastesController(AppDbContext context, DbSet<Taste> table) : base(context, table)
        {
        }
    }
}
