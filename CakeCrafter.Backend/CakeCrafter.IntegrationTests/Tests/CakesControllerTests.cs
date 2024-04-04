using CakeCrafter.API;
using CakeCrafter.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace CakeCrafter.IntegrationTests.Tests
{
    public class CakesControllerTests : IClassFixture<WebApplicationFactory<Program>>,  //этот интерфейс позволяет создать WebApplicationFactory один раз на все тестовые методы
                                        IAsyncLifetime                                  //для асинхронной инциализации, которую нельзя вызвать в конструкторе
        {
        private readonly WebApplicationFactory<Program> _app;
        private readonly IServiceScope _scope;
        private readonly HttpClient _client;
        public CakesControllerTests(WebApplicationFactory<Program> app)
        {
            _app = app.WithWebHostBuilder(webHostBuilder =>
            {
                webHostBuilder.UseEnvironment("IntegrationTests");
            });

            _scope = _app.Services.CreateScope();
            _client = _app.CreateClient();
        }

        public Task DisposeAsync()
        {
            _scope.Dispose();
            _client.Dispose();
            return Task.CompletedTask;
        }

        public async Task InitializeAsync()
        {
            var dbContext = _scope.ServiceProvider.GetRequiredService<CakeCrafterDbContext>();
            await dbContext.Database.MigrateAsync();
        }

        [Fact]
        public async Task Get_ShouldReturnOKStatusCode()
        {
            var response = await _client.GetAsync("api/cakes?categoryId=1&skip=0&take=5");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
