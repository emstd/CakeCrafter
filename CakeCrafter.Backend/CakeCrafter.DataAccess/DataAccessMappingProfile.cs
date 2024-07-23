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
            CreateMap<Cake, CakeEntity>().ReverseMap().
                ForMember(cake => cake.ImageName, opt => opt.MapFrom(x => x.ImageId == null || x.Image == null
                                                                          ? "NoImage.png"
                                                                          : $"{x.ImageId}{x.Image.Extension}"));
            CreateMap<Image, ImageEntity>();
            CreateMap<User, UserEntity>().ReverseMap();
        }
    }
}
