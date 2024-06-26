using CakeCrafter.API.Extensions;
using CakeCrafter.API.Options;
using CakeCrafter.DataAccess;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CakeCrafter.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(builder.Configuration)
                    .CreateLogger();

            builder.Services.AddSerilog();

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
                    p.WithOrigins("http://127.0.0.1:5173")
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

            builder.Services.AddHttpClient();

            builder.Services.AddRepositories();
            builder.Services.AddServices();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.Configure<ImageHostSettings>(builder.Configuration.GetSection(ImageHostSettings.SectionName));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();
            app.UseCors("AllowCakeCrafterApp");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
