using CakeCrafter.Core.Interfaces.Services;
using CakeCrafter.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CakeCrafter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImagesController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost("imageFile")]
        public async Task<ActionResult<Guid>> DownloadImage([Required] IFormFile image)
        {
            if (image is not { Length: > 0 })
            {
                return BadRequest();
            }

            await using var imageStream = image.OpenReadStream();
            string imgExtension = Path.GetExtension(image.FileName);
            await using Image img = new Image(imageStream, imgExtension);
            var imageId = await _imageService.CreateImage(img);

            return Ok(imageId);
        }

        [HttpPost("imageUrl")]
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
                string imgExtension = Path.GetExtension(imageUrl) ?? ".jpg";
                await using var image = new Image(imageStream, imgExtension);
                var imageId = await _imageService.CreateImage(image);

                return Ok(imageId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
