using AutoMapper;
using CakeCrafter.Core.Interfaces.Repositories;
using CakeCrafter.Core.Models;
using CakeCrafter.DataAccess.Entites;

namespace CakeCrafter.DataAccess.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly IMapper _mapper;
        private readonly CakeCrafterDbContext _context;

        public ImageRepository(IMapper mapper, CakeCrafterDbContext context)
        {
           _mapper = mapper;
           _context = context;
        }

        public async Task<Guid> CreateImage(Image image)
        {
            var newImage = _mapper.Map<Image, ImageEntity>(image);
            await _context.Images.AddAsync(newImage);
            await _context.SaveChangesAsync();
            return newImage.Id;
        }
    }
}
