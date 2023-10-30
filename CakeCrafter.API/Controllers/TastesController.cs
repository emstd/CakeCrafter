using CakeCrafter.API.Data;
using CakeCrafter.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CakeCrafter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TastesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TastesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Taste>>> GetTastes()
        {
            return Ok(await _context.Tastes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Taste>> GetTaste(int id)
        {
            var taste = await _context.Tastes.FindAsync(id);
            if (taste == null)
            {
                return BadRequest("Taste not found!");
            }
            return Ok(taste);
        }

        [HttpPost]
        public async Task<ActionResult<List<Taste>>> AddTaste(Taste taste)
        {
            _context.Add(taste);
            await _context.SaveChangesAsync();
            return Ok(await _context.Tastes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Taste>>> UpdateTaste(Taste inputTaste)
        {
            var dbTaste = await _context.Tastes.FindAsync(inputTaste.Id);
            if (dbTaste == null)
            {
                return BadRequest("Taste not found");
            }
            dbTaste.Name = inputTaste.Name;
            await _context.SaveChangesAsync();
            return Ok(await _context.Tastes.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<Taste>>> DeleteTaste(int id)
        {
            var dbTaste = await _context.Tastes.FindAsync(id);
            if (dbTaste == null)
            {
                return BadRequest("Taste not found!");
            }
            _context.Tastes.Remove(dbTaste);
            await _context.SaveChangesAsync();
            return Ok(await _context.Tastes.ToListAsync());
        }
    }
}
