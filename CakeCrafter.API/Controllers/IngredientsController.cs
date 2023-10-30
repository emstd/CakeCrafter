using CakeCrafter.API.Data;
using CakeCrafter.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace CakeCrafter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public IngredientsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Ingredient>>> GetIngredients()
        {
            return Ok(await _context.Ingredients.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ingredient>> GetIngredient(int id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient == null)
            {
                return BadRequest("Ingredient not found!");
            }
            return Ok(ingredient);
        }

        [HttpPost]
        public async Task<ActionResult<List<Ingredient>>> AddIngredient(Ingredient ingredient)
        {
            _context.Ingredients.Add(ingredient);
            await _context.SaveChangesAsync();
            return Ok(await _context.Ingredients.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Ingredient>>> UpdateIngredient(Ingredient inputIngredient)
        {
            var dbIngredient = await _context.Ingredients.FindAsync(inputIngredient.Id);
            if (dbIngredient == null)
            {
                return BadRequest("Ingredient not found!");
            }
            dbIngredient.Name = inputIngredient.Name;
            dbIngredient.Price = inputIngredient.Price;

            await _context.SaveChangesAsync();

            return Ok(await _context.Ingredients.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Ingredient>>> DeleteIngredient(int id)
        {
            var dbIngredient = await _context.Ingredients.FindAsync(id);
            if (dbIngredient == null)
            {
                return BadRequest("Ingredient not found!");
            }
            _context.Ingredients.Remove(dbIngredient);
            await _context.SaveChangesAsync();

            return Ok(await _context.Ingredients.ToListAsync());
        }
    }
}
