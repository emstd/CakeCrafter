using CakeCrafter.API;
using CakeCrafter.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace CakeCrafter.IntegrationTests.Tests
{
    public class CakesControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _app;
        private readonly IServiceScope _scope;
        private readonly HttpClient _client;
        public CakesControllerTests(WebApplicationFactory<Program> app)
        {
            _app = app.WithWebHostBuilder(configuration =>
            {
                configuration.UseEnvironment("IntegrationTests");
            });

            _scope = _app.Services.CreateScope();
            _client = _app.CreateClient();
        }

        [Fact]
        public async Task Test1()
        {
            var dbContext = _scope.ServiceProvider.GetRequiredService<CakeCrafterDbContext>();
            await dbContext.Database.MigrateAsync();

            var response = await _client.GetAsync("api/cakes?categoryId=1&skip=0&take=5");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
