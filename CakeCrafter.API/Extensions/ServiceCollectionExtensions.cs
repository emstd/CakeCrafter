using CakeCrafter.Core.Interfaces.Repositories;
using CakeCrafter.Core.Interfaces.Services;
using CakeCrafter.Core.Models;
using CakeCrafter.DataAccess.Repositories;
using CakeCrafter.BusinessLogic;
namespace CakeCrafter.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICakeRepository, ICakeRepository>();
            services.AddScoped<IGenericRepository<CakesIngredients>, GenericRepository<CakesIngredients>>();
            services.AddScoped<IGenericRepository<Category>, GenericRepository<Category>>();
            services.AddScoped<IGenericRepository<Ingredient>, GenericRepository<Ingredient>>();
            services.AddScoped<IGenericRepository<IngredientCategory>, GenericRepository<IngredientCategory>>();
            services.AddScoped<IGenericRepository<MeasureUnit>, GenericRepository<MeasureUnit>>();
            services.AddScoped<IGenericRepository<Taste>, GenericRepository<Taste>>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICakeService, ICakeService>();
            services.AddScoped<IGenericService<CakesIngredients>, GenericService<CakesIngredients>>();
            services.AddScoped<IGenericService<Category>, GenericService<Category>>();
            services.AddScoped<IGenericService<Ingredient>, GenericService<Ingredient>>();
            services.AddScoped<IGenericService<IngredientCategory>, GenericService<IngredientCategory>>();
            services.AddScoped<IGenericService<MeasureUnit>, GenericService<MeasureUnit>>();
            services.AddScoped<IGenericService<Taste>, GenericService<Taste>>();

            return services;
        }
    }
}
