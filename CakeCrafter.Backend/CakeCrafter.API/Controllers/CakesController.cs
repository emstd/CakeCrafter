using AutoMapper;
using CakeCrafter.API.Contracts;
using CakeCrafter.Core.Interfaces.Services;
using CakeCrafter.Core.Models;
using CakeCrafter.Core.Pages;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace CakeCrafter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CakesController : ControllerBase
    {
        private readonly ICakeService _service;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CakesController(ICakeService service, IMapper mapper, IWebHostEnvironment _host)
        {
            _service = service;
            _mapper = mapper;
            _webHostEnvironment = _host;
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
        public async Task<ActionResult<int>> CreateCake([FromForm] CakeCreateRequest cakeCreate)
        {
            var cake = _mapper.Map<CakeCreateRequest, Cake>(cakeCreate);

            if (cakeCreate.Image != null && cakeCreate.Image.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(cakeCreate.Image.FileName);
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, fileName);


                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await cakeCreate.Image.CopyToAsync(fileStream);
                }
                cake.ImageURL = fileName;
            }

            return Ok(await _service.Create(cake));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CakeCreateRequest>> UpdateCake([FromForm] CakeUpdateRequest cakeUpdate, int id)
        {
            var cake = _mapper.Map<CakeUpdateRequest, Cake>(cakeUpdate);
            cake.Id = id;
            if (cakeUpdate.Image != null && cakeUpdate.Image.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(cakeUpdate.Image.FileName);
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, fileName);


                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await cakeUpdate.Image.CopyToAsync(fileStream);
                }
                cake.ImageURL = fileName;
            }
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
