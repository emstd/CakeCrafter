using AutoMapper;
using CakeCrafter.API.Contracts;
using CakeCrafter.API.Options;
using CakeCrafter.BusinessLogic;
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
        public async Task<ActionResult<ItemsPage<CakeGetResponse>>> GetCakes([FromQuery] int categoryId, 
                                                                             [FromQuery] int skip, 
                                                                             [FromQuery] int take, 
                                                                             [FromServices]IOptions<URLs> _URLs)
        {
            var cakesPage = await _service.Get(categoryId, skip, take);
            URLs urls = _URLs.Value;
            var PageResponse = new ItemsPage<CakeGetResponse>
            {
                Items = cakesPage.Items.Select(cake => _mapper.Map<Cake, CakeGetResponse>(cake))
                                       .Select(cake =>
                                       {
                                           cake.ImageUrl = cake.ImageId == null ? urls.ImagesURL + "NoImage.png" : urls.ImagesURL + cake.ImageUrl;
                                           return cake;
                                       })
                                       .ToArray(),
                TotalItems = cakesPage.TotalItems
            };
            return Ok(PageResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CakeGetResponse>> GetCakeById(int id, [FromServices] IOptions<URLs> _URLs)
        {
            URLs urls = _URLs.Value;
            var cake = await _service.GetById(id);
            if (cake == null)
            {
                return NotFound();
            }
            var CakeResponse = _mapper.Map<Cake, CakeGetResponse>(cake);
            CakeResponse.ImageUrl = CakeResponse.ImageId == null ? urls.ImagesURL + "NoImage.png" : urls.ImagesURL + CakeResponse.ImageUrl;
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

        [HttpPost("ImageByUrl")]
        public async Task<ActionResult<string>> CreateImage([FromBody] string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                return BadRequest();
            }
            try
            {
                using (var httpClient = new HttpClient())                               //Загружаем картинку с указанного URL и сохраняем на сервер,
                {                                                                       //генерируя вместо названия GUID
                    var imageBytes = await httpClient.GetByteArrayAsync(imageUrl);

                    string imgExtension = ".jpg";                                                       //Расширение .jpg будет у всех скаченных картинок
                    Guid imgId = Guid.NewGuid();
                    string fileName = imgId.ToString() + imgExtension;
                    var path = Path.Combine(_webHostEnvironment.WebRootPath, "Resources", "Images", fileName);    //Путь: /wwwroot/Resources/Images

                    /*using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await fileStream.WriteAsync(imageBytes, 0, imageBytes.Length);
                    }*/
                    System.IO.File.WriteAllBytes(path, imageBytes);                 //Сохраняем картинку на сервере

                    Image image = new Image()
                    {
                        Id = imgId,
                        Extension = imgExtension,
                    };
                    Guid imageId = await _imageService.CreateImage(image);
                    return Ok(imageId);
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<CakeGetResponse>> UpdateCake(CakeUpdateRequest cakeUpdate, int id, [FromServices]IOptions<URLs> _URLs)
        {
            URLs urls = _URLs.Value;
            var cake = _mapper.Map<CakeUpdateRequest, Cake>(cakeUpdate);
            cake.Id = id;
            var updatedCake = await _service.Update(cake);
            if (updatedCake == null)
            {
                return NotFound();
            }
            var CakeResponse = _mapper.Map<Cake, CakeGetResponse>(updatedCake);
            CakeResponse.ImageUrl = CakeResponse.ImageId == null ? urls.ImagesURL + "NoImage.png" : urls.ImagesURL + CakeResponse.ImageId;
            return Ok(CakeResponse);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Cake>>> DeleteCake(int id)
        {
            var result = await _service.Delete(id);
            return Ok(result);
        }
    }
}
