using CakeCrafter.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CakeCrafter.API.Controllers.Templates
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class InformationController<TModel> : ControllerBase where TModel : class
    {
        protected readonly CakeCrafterDbContext _context;
        private DbSet<TModel> _table;

        public InformationController(CakeCrafterDbContext context)
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
        public async Task<ActionResult<List<TModel>>> AddModelAsync(TModel model)
        {
            _table.Add(model);
            await _context.SaveChangesAsync();
            return Ok(await _table.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<TModel>>> UpdateModelAsync(TModel model, [FromRoute] int id)
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
        public async Task<ActionResult<List<TModel>>> DeleteModelAsync([FromRoute] int id)
        {
            var dbModel = await _table.FindAsync(id);
            if (dbModel == null)
            {
                return Ok();
            }
            _table.Remove(dbModel);
            await _context.SaveChangesAsync();
            return Ok(await _table.ToListAsync());
        }
    }
}
