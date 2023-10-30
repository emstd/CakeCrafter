using CakeCrafter.API.Data;
using CakeCrafter.API.Models;
using CakeCrafter.API.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CakeCrafter.API.Controllers.Templates
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class InformationController<TModel> : ControllerBase where TModel : class, IInfo
    {
        protected readonly AppDbContext _context;
        private DbSet<TModel> _table;

        public InformationController(AppDbContext context, DbSet<TModel> table)
        {
            _context = context;
            _table = table;
        }

        [HttpGet]
        public async Task<ActionResult<List<TModel>>> GetModelsAsync()
        {
            return Ok(await _table.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TModel>> GetModelByIdAsync(int id)
        {
            var dbModel = await _table.FindAsync(id);
            if (dbModel == null)
            {
                return NotFound();
            }
            return Ok(dbModel);
        }

        [HttpPost]
        public async Task<ActionResult<List<Category>>> AddModelAsync(TModel model)
        {
            _table.Add(model);
            await _context.SaveChangesAsync();
            return Ok(await _table.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Category>>> UpdateModelAsync(TModel model)
        {
            var dbModel = await _table.FindAsync(model.Id);
            if (dbModel == null)
            {
                return NotFound();
            }
            dbModel.Name = model.Name;
            await _context.SaveChangesAsync();
            return Ok(await _table.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<Category>>> DeleteModelAsync(int id)
        {
            var dbModel = await _table.FindAsync(id);
            if (dbModel == null)
            {
                return NotFound();
            }
            _table.Remove(dbModel);
            await _context.SaveChangesAsync();
            return Ok(await _table.ToListAsync());
        }
    }
}
