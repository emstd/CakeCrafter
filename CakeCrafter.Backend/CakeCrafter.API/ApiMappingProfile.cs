using AutoMapper;
using CakeCrafter.API.Contracts;
using CakeCrafter.Core.Models;

namespace CakeCrafter.API
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<CakeCreate, Cake>();
            CreateMap<Cake, CakeGet>();
            CreateMap<CakeUpdate, Cake>();
        }
    }
}
