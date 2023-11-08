using CakeCrafter.API.Data;
using CakeCrafter.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CakeCrafter.API.Controllers.Templates
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class InformationController<TModel> : ControllerBase where TModel : class
    {
        protected readonly AppDbContext _context;
        private DbSet<TModel> _table;

        public InformationController(AppDbContext context)
        {
            _context = context;
            _table = context.Set<TModel>();
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

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Category>>> UpdateModelAsync(TModel model, [FromRoute] int id)
        {
            var dbModel = await _table.FindAsync(id);
            if (dbModel == null)
            {
                return NotFound();
            }
            _context.Update(model);
            await _context.SaveChangesAsync();
            return Ok(await _table.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Category>>> DeleteModelAsync([FromRoute] int id)
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
