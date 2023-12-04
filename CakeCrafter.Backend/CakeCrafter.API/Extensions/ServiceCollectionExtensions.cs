using CakeCrafter.Core.Interfaces.Repositories;
using CakeCrafter.Core.Interfaces.Services;
using CakeCrafter.Core.Models;
using CakeCrafter.DataAccess.Repositories;
using CakeCrafter.BusinessLogic;
using CakeCrafter.Domain;
namespace CakeCrafter.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICakeRepository, CakeRepository>();
            services.AddScoped<IGenericRepository<Category>, GenericRepository<Category>>();
            services.AddScoped<IGenericRepository<Taste>, GenericRepository<Taste>>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICakeService, CakeService>();
            services.AddScoped<IGenericService<Category>, GenericService<Category>>();
            services.AddScoped<IGenericService<Taste>, GenericService<Taste>>();

            return services;
        }
    }
}
