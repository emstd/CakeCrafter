using AutoMapper;
using CakeCrafter.API.Contracts;
using CakeCrafter.API.Options;
using CakeCrafter.Core.Interfaces.Services;
using CakeCrafter.Core.Models;
using CakeCrafter.Core.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CakeCrafter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CakesController : ControllerBase
    {
        private readonly ICakeService _service;
        private readonly IMapper _mapper;
        public IOptions<ImageHostSettings> _imageHostSettings;

        public CakesController(ICakeService service, IMapper mapper, IOptions<ImageHostSettings> imageHostSettings)
        {
            _service = service;
            _mapper = mapper;
            _imageHostSettings = imageHostSettings;
        }

        [HttpGet]
        public async Task<ActionResult<ItemsPage<CakeGetResponse>>> GetCakes([FromQuery] int categoryId,
                                                                             [FromQuery] int skip,
                                                                             [FromQuery] int take)
        {
            if (categoryId < 1 || skip < 0 || take < 0)
            {
                return BadRequest();
            }

            var cakesPage = await _service.Get(categoryId, skip, take);
            var imageSettings = _imageHostSettings.Value;
            var cakesResponse = new ItemsPage<CakeGetResponse>
            {
                Items = cakesPage.Items.Select(cake =>
                    {
                        var cakeResponse = _mapper.Map<Cake, CakeGetResponse>(cake);
                        cakeResponse.ImageUrl = cake.GetImageUrl(imageSettings.ImageHostUrl);
                        return cakeResponse;
                    })
                    .ToArray(),
                TotalItems = cakesPage.TotalItems
            };
            return Ok(cakesResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CakeGetResponse>> GetCakeById(int id)
        {
            if (id == 0 || id < 0)
            {
                return BadRequest();
            }

            var imageSettings = _imageHostSettings.Value;
            var cake = await _service.GetById(id);
            if (cake == null)
            {
                return NotFound();
            }
            var cakeResponse = _mapper.Map<Cake, CakeGetResponse>(cake);
            cakeResponse.ImageUrl = cake.GetImageUrl(imageSettings.ImageHostUrl);
            return Ok(cakeResponse);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateCake(CakeCreateRequest cakeCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var cake = _mapper.Map<CakeCreateRequest, Cake>(cakeCreate);
            return Ok(await _service.Create(cake));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CakeGetResponse>> UpdateCake(CakeUpdateRequest cakeUpdate, int id)
        {
            if (!ModelState.IsValid || id == 0 || id < 0)
            {
                return BadRequest();
            }

            ImageHostSettings imageSettings = _imageHostSettings.Value;
            var cake = _mapper.Map<CakeUpdateRequest, Cake>(cakeUpdate);
            cake.Id = id;
            var updatedCake = await _service.Update(cake);

            if (updatedCake == null)
            {
                return NotFound();
            }
            var cakeResponse = _mapper.Map<Cake, CakeGetResponse>(updatedCake);
            cakeResponse.ImageUrl = updatedCake.GetImageUrl(imageSettings.ImageHostUrl);
            return Ok(cakeResponse);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Cake>>> DeleteCake(int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }

            var result = await _service.Delete(id);
            return Ok(result);
        }
    }
}
