using CakeCrafter.API.Extensions;
using CakeCrafter.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CakeCrafter.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseDefaultServiceProvider(x =>
            {
                x.ValidateScopes = true;
                x.ValidateOnBuild = true;
            });

            // Add services to the container.
            builder.Services.AddCors(o =>
            {
                o.AddPolicy("AllowCakeCrafterApp", p =>
                {
                    p.WithOrigins("http://localhost:5173")
                    .WithHeaders().AllowAnyHeader()
                    .WithMethods().AllowAnyMethod()
                    .AllowCredentials()
                    .SetIsOriginAllowedToAllowWildcardSubdomains();
                });
            });
            builder.Services.AddControllers();
            builder.Services.AddDbContext<CakeCrafterDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<DataAccessMappingProfile>();
                cfg.AddProfile<ApiMappingProfile>();
            });

            builder.Services.AddRepositories();
            builder.Services.AddServices();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseHttpLogging();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowCakeCrafterApp");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}