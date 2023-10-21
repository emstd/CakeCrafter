using CakeCrafter.API.Data;
using CakeCrafter.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace CakeCrafter.API.Controllers.Templates
{
    public class InformationController<TModel> : ControllerBase where TModel : class, IInfo
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
                return BadRequest("Category nor found!");
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
                return BadRequest("Category nor found!");
            }
            dbModel.Name = model.Name;
            await context.SaveChangesAsync();
            return Ok(await table.ToListAsync());
        }

        public async Task<ActionResult<List<Category>>> DeleteModelASync(DbSet<TModel> table, int id, AppDbContext context)
        {
            var dbModel = await table.FindAsync(id);
            if (dbModel == null)
            {
                return BadRequest("Category nor found!");
            }
            table.Remove(dbModel);
            await context.SaveChangesAsync();
            return Ok(await table.ToListAsync());
        }
    }
}
