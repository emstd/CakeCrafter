using CakeCrafter.Core.Models;
using System.Net.Mime;

namespace CakeCrafter.IntegrationTests.Tests
{
    public class CategoriesControllerTests : IClassFixture<WebApplicationFactory<Program>>, IAsyncLifetime
    {
        private readonly WebApplicationFactory<Program> _app;
        private readonly IServiceScope _scope;
        private readonly HttpClient _client;
        private readonly CakeCrafterDbContext _dbContext;
        private readonly Fixture _fixture;

        public CategoriesControllerTests(WebApplicationFactory<Program> app)
        {
            _app = app.WithWebHostBuilder(webHostBuilder =>
            {
                webHostBuilder.UseEnvironment("IntegrationTests");
            });
            _scope = _app.Services.CreateScope();
            _client = _app.CreateClient();
            _dbContext = _scope.ServiceProvider.GetRequiredService<CakeCrafterDbContext>();
            _fixture = new Fixture();
        }

        public Task DisposeAsync()
        {
            _scope.Dispose();
            _client.Dispose();
            _dbContext.Dispose();
            return Task.CompletedTask;
        }

        public async Task InitializeAsync()
        {
            await _dbContext.Database.MigrateAsync();
            var connection = _dbContext.Database.GetDbConnection();
            await connection.OpenAsync();
            var respawner = await Respawner.CreateAsync(connection, new RespawnerOptions
            {
                TablesToIgnore = new Table[]
                {
                "__EFMigrationsHistory"
                }
            });

            await respawner.ResetAsync(connection);
        }

        [Fact]
        public async Task Get_ShouldReturnOKStatusCode()
        {
            await CreateTestCategoryEntity();
            var response = await _client.GetAsync("api/categories");

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task GetById_ShouldReurnOKStatusCode()
        {
            var categoryId = await CreateTestCategoryEntity();

            var response = await _client.GetAsync($"api/categories/{categoryId}");

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Create_ShouldReturnOKStatusCode()
        {
            var categoryRequest = _fixture.Build<Category>()
                                          .Without(x => x.Id)
                                          .Create();

            var jsonCategoryRequest = JsonConvert.SerializeObject(categoryRequest);
            var requestContent = new StringContent(jsonCategoryRequest, Encoding.UTF8, MediaTypeNames.Application.Json);

            var response = await _client.PostAsync($"api/categories", requestContent);

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseCategory = JsonConvert.DeserializeObject<Category>(responseContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.IsType<Category>(responseCategory);
        }

        [Fact]
        public async Task Update_ShouldReturnOKStatusCode()
        {
            var categoryId = await CreateTestCategoryEntity();
            var categoryRequest = _fixture.Build<Category>()
                                          .Without(x => x.Id)
                                          .Create();

            var jsonCategoryRequest = JsonConvert.SerializeObject(categoryRequest);
            var requestContent = new StringContent(jsonCategoryRequest, Encoding.UTF8, MediaTypeNames.Application.Json);

            var response = await _client.PutAsync($"api/categories/{categoryId}", requestContent);

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseCategory = JsonConvert.DeserializeObject<Category>(responseContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.IsType<Category>(responseCategory);
        }

        [Fact]
        public async Task Delete_ShouldReturnOKStatusCode()
        {
            var categoryId = await CreateTestCategoryEntity();
            var response = await _client.DeleteAsync($"api/categories/{categoryId}");

            Assert.True(response.IsSuccessStatusCode);
        }

        public async Task<int> CreateTestCategoryEntity()
        {
            var testCategory = _fixture.Build<CategoryEntity>()
                                .Without(x => x.Id)
                                .Create();

            await _dbContext.Categories.AddAsync(testCategory);
            await _dbContext.SaveChangesAsync();
            _dbContext.ChangeTracker.Clear();
            return testCategory.Id;
        }

    }
}
