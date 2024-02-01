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
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IImageService _imageService;

        public CakesController(ICakeService service, 
                               IMapper mapper, 
                               IWebHostEnvironment _host, 
                               IImageService imageService)
        {
            _service = service;
            _mapper = mapper;
            _webHostEnvironment = _host;
            _imageService = imageService;
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

        [HttpPost("image")]
        public async Task<ActionResult<Guid>> DownloadImage(IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                string imgExtension = Path.GetExtension(image.FileName);
                Guid imgGuid = Guid.NewGuid();
                string fileName = imgGuid.ToString() + imgExtension;
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Resources", "Images", fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                Image img = new Image()
                {
                    Id = imgGuid,
                    Extension = imgExtension,
                };

                Guid ImageId = await _imageService.CreateImage(img);
                return Ok(ImageId);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CakeCreateRequest>> UpdateCake(CakeUpdateRequest cakeUpdate, int id)
        {
            var cake = _mapper.Map<CakeUpdateRequest, Cake>(cakeUpdate);
            cake.Id = id;
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
