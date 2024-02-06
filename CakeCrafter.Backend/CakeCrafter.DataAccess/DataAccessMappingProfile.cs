using AutoMapper;
using CakeCrafter.Core.Models;
using CakeCrafter.DataAccess.Entites;

namespace CakeCrafter.DataAccess
{
    public class DataAccessMappingProfile : Profile
    {
        public DataAccessMappingProfile()
        {
            CreateMap<Category, CategoryEntity>().ReverseMap();
            CreateMap<Taste, TasteEntity>().ReverseMap();
            CreateMap<Cake, CakeEntity>().ReverseMap();
            CreateMap<Image, ImageEntity>();
        }
    }
}
