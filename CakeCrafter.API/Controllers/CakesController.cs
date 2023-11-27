using CakeCrafter.API.Contracts;
using CakeCrafter.Core.Interfaces.Services;
using CakeCrafter.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CakeCrafter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CakesController : ControllerBase
    {
        private readonly ICakeService _service;

        public CakesController(ICakeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cake>>> GetCakes([FromRoute] string category, [FromQuery] int pageNumber = 1)
        {
            var cakes = await _service.Get(category, pageNumber);
            var result = cakes.Select(cake => new GetCakeResponse()
            {
                Id = cake.Id,
                Name = cake.Name,
                Description = cake.Description,
                Taste = cake.Taste,
                Category = cake.Category,
                CookTime = cake.CookTime,
                Level = cake.Level,
                Weight = cake.Weight,
            });
            return Ok(await _service.Get(category, pageNumber));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cake>> GetCakeById(int id)
        {
            var result = await _service.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Cake>> CreateCake(Cake cake)
        {
            var result = await _service.Create(cake);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<List<Cake>>> UpdateCake(Cake cake)
        {
            var result = await _service.Update(cake);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Cake>>> DeleteCake(int id)
        {
            var result = await _service.Delete(id);
            return Ok(result);
        }
    }
}
