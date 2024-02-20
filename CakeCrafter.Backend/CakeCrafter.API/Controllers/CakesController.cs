using AutoMapper;
using CakeCrafter.API.Contracts;
using CakeCrafter.API.Options;
using CakeCrafter.Core.Interfaces.Services;
using CakeCrafter.Core.Models;
using CakeCrafter.Core.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

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
                               IWebHostEnvironment webHostEnvironment,
                               IImageService imageService)
        {
            _service = service;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<ActionResult<ItemsPage<CakeGetResponse>>> GetCakes([FromQuery] int categoryId,
                                                                             [FromQuery] int skip,
                                                                             [FromQuery] int take,
                                                                             [FromServices] IOptions<URLs> _URLs)
        {
            var cakesPage = await _service.Get(categoryId, skip, take);
            var urls = _URLs.Value;
            var cakesResponse = new ItemsPage<CakeGetResponse>
            {
                Items = cakesPage.Items.Select(cake =>
                {
                    var cakeResponse = _mapper.Map<Cake, CakeGetResponse>(cake);
                    cakeResponse.ImageUrl = cake.GetImageUrl(urls.ImageHostUrl);
                    return cakeResponse;
                })
                .ToArray(),
                TotalItems = cakesPage.TotalItems
            };
            return Ok(cakesResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CakeGetResponse>> GetCakeById(int id, [FromServices] IOptions<URLs> URLs)
        {
            var urls = URLs.Value;
            var cake = await _service.GetById(id);
            if (cake == null)
            {
                return NotFound();
            }
            var cakeResponse = _mapper.Map<Cake, CakeGetResponse>(cake);
            cakeResponse.ImageUrl = cake.GetImageUrl(urls.ImageHostUrl);
            return Ok(cakeResponse);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateCake(CakeCreateRequest cakeCreate)
        {
            var cake = _mapper.Map<CakeCreateRequest, Cake>(cakeCreate);
            return Ok(await _service.Create(cake));
        }

        [HttpPost("image")]
        public async Task<ActionResult<Guid>> DownloadImage([Required] IFormFile image)
        {
            if (image is { Length: > 0 })
            {
                await using var imageStream = image.OpenReadStream();
                string imgExtension = Path.GetExtension(image.FileName);
                Image img = new Image(imageStream, imgExtension);
                var imageId = await _imageService.CreateImage(img);

                return Ok(imageId);
            }
            return BadRequest();
        }

        [HttpPost("ImageByUrl")]
        public async Task<ActionResult<string>> CreateImage([FromBody] string imageUrl, [FromServices] IHttpClientFactory clientFactory)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                return BadRequest();
            }
            try
            {
                using var httpClient = clientFactory.CreateClient();
                await using var imageStream = await httpClient.GetStreamAsync(imageUrl);
                await using var image = new Image(imageStream);
                var imageId = await _imageService.CreateImage(image);
                return Ok(imageId);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<CakeGetResponse>> UpdateCake(CakeUpdateRequest cakeUpdate, int id, [FromServices] IOptions<URLs> URLs)
        {
            URLs urls = URLs.Value;
            var cake = _mapper.Map<CakeUpdateRequest, Cake>(cakeUpdate);
            cake.Id = id;
            var updatedCake = await _service.Update(cake);
            if (updatedCake == null)
            {
                return NotFound();
            }
            var cakeResponse = _mapper.Map<Cake, CakeGetResponse>(updatedCake);
            cakeResponse.ImageUrl = updatedCake.GetImageUrl(urls.ImageHostUrl);
            return Ok(cakeResponse);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Cake>>> DeleteCake(int id)
        {
            var result = await _service.Delete(id);
            return Ok(result);
        }
    }
}
