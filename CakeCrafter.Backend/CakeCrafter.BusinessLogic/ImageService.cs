using CakeCrafter.Core.Interfaces.Repositories;
using CakeCrafter.Core.Interfaces.Services;
using CakeCrafter.Core.Models;

namespace CakeCrafter.BusinessLogic
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _repository;

        public ImageService(IImageRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> CreateImage(Image image)
        {
            var fileName = image.Name;
            var path = Path.Combine("wwwroot", "Resources", "Images", fileName);

            await using(var fileStream = new FileStream(path, FileMode.Create))
            {
                await image.Content.CopyToAsync(fileStream);
            }
            return await _repository.CreateImage(image);
        }
    }
}
