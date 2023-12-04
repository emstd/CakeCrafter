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
        public async Task<ActionResult<List<Cake>>> GetCakes([FromQuery] int categoryId, [FromQuery] int skip, [FromQuery] int take)
        {
            var cakes = await _service.Get(categoryId, skip, take);
            var result = cakes.Select(cake => new GetCakeResponse()
            {
                Id = cake.Id,
                Name = cake.Name,
                Description = cake.Description,
                TasteId = cake.TasteId,
                CategoryId = cake.CategoryId,
                CookTimeInMinutes = cake.CookTimeInMinutes,
                Level = cake.Level,
                Weight = cake.Weight,
            });
            return Ok(await _service.Get(categoryId, skip, take));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cake>> GetCakeById(int id)
        {
            var result = await _service.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            var cake = new GetCakeResponse
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                TasteId = result.TasteId,
                CategoryId = result.CategoryId,
                CookTimeInMinutes = result.CookTimeInMinutes,
                Level = result.Level,
                Weight = result.Weight,
            };
            return Ok(cake);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateCake(CreateCakeRequest cakeRequest)
        {
            var cake = new Cake{
                Name = cakeRequest.Name,
                Description = cakeRequest.Description,
                TasteId = cakeRequest.TasteId,
                CategoryId = cakeRequest.CategoryId,
                CookTimeInMinutes = cakeRequest.CookTimeInMinutes,
                Level = cakeRequest.Level,
                Weight = cakeRequest.Weight
            };
            return Ok(await _service.Create(cake));
        }

        [HttpPut]
        public async Task<ActionResult<List<Cake>>> UpdateCake(CreateCakeRequest cakeRequest)
        {
            var cake = new Cake
            {
                Name = cakeRequest.Name,
                Description = cakeRequest.Description,
                TasteId = cakeRequest.TasteId,
                CategoryId = cakeRequest.CategoryId,
                CookTimeInMinutes = cakeRequest.CookTimeInMinutes,
                Level = cakeRequest.Level,
                Weight = cakeRequest.Weight
            };

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
