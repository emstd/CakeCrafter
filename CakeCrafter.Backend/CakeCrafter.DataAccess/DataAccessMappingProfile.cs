using AutoMapper;
using CakeCrafter.Core.Models;
using CakeCrafter.DataAccess.Entites;

namespace CakeCrafter.DataAccess
{
    public class DataAccessMappingProfile : Profile
    {
        public DataAccessMappingProfile()
        {
            CreateMap<Category, IEntity>().ReverseMap();
            CreateMap<Taste, IEntity>().ReverseMap();
        }
    }
}
