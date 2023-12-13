using CakeCrafter.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CakeCrafter.API.Controllers.Templates
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class InformationController<TModel> : ControllerBase where TModel : class
    {
        private readonly IGenericService<TModel> _service;

        public InformationController(IGenericService<TModel> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<TModel>>> GetModelsAsync()
        {
            return Ok(await _service.Get());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TModel>> GetModelByIdAsync(int id)
        {
            var dbModel = await _service.GetById(id);
            if (dbModel == null)
            {
                return NotFound();
            }
            return Ok(dbModel);
        }

        [HttpPost]
        public async Task<ActionResult<TModel>> AddModelAsync(TModel model)
        {
            var result = await _service.Create(model);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<TModel>>> UpdateModelAsync(int id, TModel model)
        {
            var dbModel = await _service.Update(model, id);
            if (dbModel == null)
            {
                return NotFound();
            }
            return Ok(dbModel);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<TModel>>> DeleteModelAsync(int id)
        {
            return Ok(await _service.Delete(id));
        }
    }
}
