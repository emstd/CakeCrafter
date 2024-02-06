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
            return await _repository.CreateImage(image);
        }
    }
}
