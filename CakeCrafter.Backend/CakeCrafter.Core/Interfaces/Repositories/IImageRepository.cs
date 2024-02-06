using CakeCrafter.Core.Models;

namespace CakeCrafter.Core.Interfaces.Repositories
{
    public interface IImageRepository
    {
        Task<Guid> CreateImage(Image image);
    }
}
