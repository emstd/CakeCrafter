using CakeCrafter.API.Data;
using CakeCrafter.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CakeCrafter.API.Controllers.Templates
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class InformationConttroller<TModel> : ControllerBase where TModel : class, IInfo
    {
        public async Task<ActionResult<List<TModel>>> GetModelsAsync(DbSet<TModel> table)
        {
            return Ok(await table.ToListAsync());
        }

        public async Task<ActionResult<TModel>> GetModelByIdAsync(DbSet<TModel> table, int id)
        {
            var dbModel = await table.FindAsync(id);
            if (dbModel == null)
            {
                return NotFound();
            }
            return Ok(dbModel);
        }

        public async Task<ActionResult<List<Category>>> AddModelAsync(TModel model, DbSet<TModel> table, AppDbContext context)
        {
            table.Add(model);
            await context.SaveChangesAsync();
            return Ok(await table.ToListAsync());
        }

        public async Task<ActionResult<List<Category>>> UpdateModelAsync(TModel model, DbSet<TModel> table, AppDbContext context)
        {
            var dbModel = await table.FindAsync(model.Id);
            if (dbModel == null)
            {
                return NotFound();
            }
            dbModel.Name = model.Name;
            await context.SaveChangesAsync();
            return Ok(await table.ToListAsync());
        }

        public async Task<ActionResult<List<Category>>> DeleteModelAsync(DbSet<TModel> table, int id, AppDbContext context)
        {
            var dbModel = await table.FindAsync(id);
            if (dbModel == null)
            {
                return NotFound();
            }
            table.Remove(dbModel);
            await context.SaveChangesAsync();
            return Ok(await table.ToListAsync());
        }
    }
}
