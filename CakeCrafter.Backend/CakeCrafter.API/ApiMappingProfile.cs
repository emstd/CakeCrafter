using AutoMapper;
using CakeCrafter.API.Contracts;
using CakeCrafter.Core.Models;

namespace CakeCrafter.API
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<CakeCreateRequest, Cake>();
            CreateMap<Cake, CakeGetResponse>();
            CreateMap<CakeUpdateRequest, Cake>();
        }
    }
}
