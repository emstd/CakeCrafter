using CakeCrafter.API.Controllers.Templates;
using CakeCrafter.API.Data;
using CakeCrafter.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CakeCrafter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        private InformationController<Category> info = new();

        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetCategories()
        {
            return await info.GetModelsAsync(_context.Categories); ;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            return await info.GetModelByIdAsync(_context.Categories, id);
        }

        [HttpPost]
        public async Task<ActionResult<List<Category>>> AddCategory(Category category)
        {
            return await info.AddModelAsync(category, _context.Categories, _context);
        }

        [HttpPut]
        public async Task<ActionResult<List<Category>>> UpdateCategory(Category inputCategory)
        {
            return await info.UpdateModelAsync(inputCategory, _context.Categories, _context);
        }

        [HttpDelete]
        public async Task<ActionResult<List<Category>>> DeleteCategory(int id)
        {
            return await info.DeleteModelASync(_context.Categories, id, _context);
        }
    }
}
