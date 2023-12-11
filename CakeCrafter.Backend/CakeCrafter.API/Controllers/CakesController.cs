using AutoMapper;
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
        private readonly IMapper _mapper;

        public CakesController(ICakeService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetCakeResponse>>> GetCakes([FromQuery] int categoryId, [FromQuery] int skip, [FromQuery] int take)
        {
            var cakes = await _service.Get(categoryId, skip, take);
            var CakesResponse = cakes.Select(cake => _mapper.Map<GetCakeResponse>(cake)).ToList();
            return Ok(CakesResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCakeResponse>> GetCakeById(int id)
        {
            var cake = await _service.GetById(id);
            if (cake == null)
            {
                return NotFound();
            }
            var CakeResponse = _mapper.Map<GetCakeResponse>(cake);
            return Ok(CakeResponse);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateCake(CreateCakeRequest cakeRequest)
        {
            var cake = _mapper.Map<Cake>(cakeRequest);
            return Ok(await _service.Create(cake));
        }

        [HttpPut]
        public async Task<ActionResult<CreateCakeRequest>> UpdateCake(CreateCakeRequest cakeRequest)
        {
            var cake = _mapper.Map<Cake>(cakeRequest);
            var CakeRequest = await _service.Update(cake);
            return Ok(CakeRequest);
        }

        [HttpDelete]
        public async Task<ActionResult<List<Cake>>> DeleteCake([FromQuery] int id)
        {
            var result = await _service.Delete(id);
            return Ok(result);
        }
    }
}
