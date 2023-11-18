using CakeCrafter.Core.Interfaces;
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
        public async Task<ActionResult<List<Cake>>> GetCakes()
        {
            return Ok(await _service.Get());
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
        public async Task<ActionResult<List<Cake>>> CreateCake(Cake cake)
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
            var result = _service.Delete(id);
            return Ok(result);
        }
    }
}
