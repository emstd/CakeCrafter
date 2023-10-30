using CakeCrafter.API.Data;
using CakeCrafter.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CakeCrafter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientCategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public IngredientCategoriesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<IngredientCategory>>> GetIngredientCategories()
        {
            return Ok(await _context.IngredientCategories.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IngredientCategory>> GetIngredientCategory(int id)
        {
            var ingredientCategory = await _context.IngredientCategories.FindAsync(id);
            if(ingredientCategory == null)
            {
                return BadRequest("Ingredient Category not found!");
            }
            return Ok(ingredientCategory);
        }

        [HttpPost]
        public async Task<ActionResult<List<IngredientCategory>>> AddIngredientCategory(IngredientCategory inputIngredientCategory)
        {
            _context.IngredientCategories.Add(inputIngredientCategory);
            await _context.SaveChangesAsync();
            return Ok(await _context.IngredientCategories.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<IngredientCategory>>> UpdateIngredientCategory(IngredientCategory inputIngredientCategory)
        {
            var dbIngredientCategory = await _context.IngredientCategories.FindAsync(inputIngredientCategory.Id);
            if (dbIngredientCategory == null)
            {
                return BadRequest("Ingredient Category not found!");
            }
            dbIngredientCategory.Name = inputIngredientCategory.Name;
            await _context.SaveChangesAsync();
            return Ok(await _context.IngredientCategories.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<IngredientCategory>>> DeleteIngredientCategory(int id)
        {
            var dbIngredientCategory = await _context.IngredientCategories.FindAsync(id);
            if (dbIngredientCategory == null)
            {
                return BadRequest("Ingredient Category not found!");
            }
            _context.IngredientCategories.Remove(dbIngredientCategory);
            await _context.SaveChangesAsync();
            return Ok(await _context.IngredientCategories.ToListAsync());
        }
    }
}
