using CakeCrafter.Core.Models;

namespace CakeCrafter.Core.Interfaces.Services
{
    public interface IImageService
    {
        Task<Guid> CreateImage(Image image);
    }
}
