using CakeCrafter.API.Controllers.Templates;
using CakeCrafter.API.Data;
using CakeCrafter.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CakeCrafter.API.Controllers
{
    public class CategoryController : InformationConttroller<Category>
    {
        public CategoryController(AppDbContext context) : base(context) { }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetCategories()
        {
            return await GetModelsAsync(_context.Categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            return await GetModelByIdAsync(_context.Categories, id);
        }

        [HttpPost]
        public async Task<ActionResult<List<Category>>> AddCategory(Category category)
        {
            return await AddModelAsync(category, _context.Categories, _context);
        }

        [HttpPut]
        public async Task<ActionResult<List<Category>>> UpdateCategory(Category inputCategory)
        {
            return await UpdateModelAsync(inputCategory, _context.Categories, _context);
        }

        [HttpDelete]
        public async Task<ActionResult<List<Category>>> DeleteCategory(int id)
        {
            return await DeleteModelAsync(_context.Categories, id, _context);
        }
    }
}
