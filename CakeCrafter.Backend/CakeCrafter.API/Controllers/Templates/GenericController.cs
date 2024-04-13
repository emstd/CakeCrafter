using CakeCrafter.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CakeCrafter.API.Controllers.Templates
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class GenericController<TModel> : ControllerBase where TModel : class
    {
        private readonly IGenericService<TModel> _service;

        public GenericController(IGenericService<TModel> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<TModel>>> GetModels()
        {
            return Ok(await _service.Get());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TModel>> GetModelById(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var dbModel = await _service.GetById(id);
            if (dbModel == null)
            {
                return NotFound();
            }
            return Ok(dbModel);
        }

        [HttpPost]
        public async Task<ActionResult<TModel>> AddModel(TModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _service.Create(model);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<TModel>>> UpdateModel(int id, TModel model)
        {
            if (!ModelState.IsValid || id < 1)
            {
                return BadRequest();
            }

            var dbModel = await _service.Update(model, id);

            if (dbModel == null)
            {
                return NotFound();
            }

            return Ok(dbModel);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<TModel>>> DeleteModel(int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }

            return Ok(await _service.Delete(id));
        }
    }
}
