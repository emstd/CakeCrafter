using CakeCrafter.API.Data;
using CakeCrafter.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CakeCrafter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CakesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CakesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cake>>> GetCakes()
        {
            return Ok(await _context.Cakes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cake>> GetCakeById(int id)
        {
            var dbCake = await _context.Cakes.FindAsync(id);
            if (dbCake == null)
            {
                return NotFound();
            }

            return Ok(dbCake);
        }

        [HttpPost]
        public async Task<ActionResult<List<Cake>>> AddCake(Cake cake)
        {
            _context.Cakes.Add(cake);
            await _context.SaveChangesAsync();
            return Ok(await _context.Cakes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Cake>>> UpdateCake(Cake cake)
        {
            var dbCake = await _context.Cakes.FindAsync(cake.Id);
            if (dbCake == null)
            {
                return NotFound();
            }

            _context.Cakes.Update(dbCake);
            await _context.SaveChangesAsync();
            return Ok(await _context.Cakes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Cake>>> DeleteCake(int id)
        {
            var dbCake = await _context.Cakes.FindAsync(id);
            if (dbCake == null)
            {
                return Ok();
            }
            _context.Cakes.Remove(dbCake);
            await _context.SaveChangesAsync();
            return Ok(await _context.Cakes.ToListAsync());
        }
    }
}
