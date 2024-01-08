using AutoMapper;
using CakeCrafter.API.Contracts;
using CakeCrafter.Core.Interfaces.Services;
using CakeCrafter.Core.Models;
using CakeCrafter.Core.Pages;
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
        public async Task<ActionResult<ItemsPage<CakeGetResponse>>> GetCakes([FromQuery] int categoryId, [FromQuery] int skip, [FromQuery] int take)
        {
            var cakesPage = await _service.Get(categoryId, skip, take);

            var PageResponse = new ItemsPage<CakeGetResponse>
            {
                Items = cakesPage.Items.Select(cake => _mapper.Map<Cake, CakeGetResponse>(cake)).ToArray(),
                TotalItems = cakesPage.TotalItems
            };

            return Ok(PageResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CakeGetResponse>> GetCakeById(int id)
        {
            var cake = await _service.GetById(id);
            if (cake == null)
            {
                return NotFound();
            }
            var CakeResponse = _mapper.Map<Cake, CakeGetResponse>(cake);
            return Ok(CakeResponse);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateCake(CakeCreateRequest cakeCreate)
        {
            var cake = _mapper.Map<CakeCreateRequest, Cake>(cakeCreate);
            return Ok(await _service.Create(cake));
        }

        [HttpPut]
        public async Task<ActionResult<CakeCreateRequest>> UpdateCake(CakeUpdateRequest cakeUpdate)
        {
            var cake = _mapper.Map<CakeUpdateRequest, Cake>(cakeUpdate);
            var updatedCake = await _service.Update(cake);
            if (updatedCake == null)
            {
                return NotFound();
            }
            return Ok(updatedCake);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Cake>>> DeleteCake(int id)
        {
            var result = await _service.Delete(id);
            return Ok(result);
        }
    }
}
