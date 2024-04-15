using CakeCrafter.Core.Pages;

namespace CakeCrafter.IntegrationTests.Tests
{
    public class CakesControllerTests : IClassFixture<WebApplicationFactory<Program>>,  //Этот интерфейс позволяет создать WebApplicationFactory один раз на все тестовые методы
                                        IAsyncLifetime                                  //Этот для асинхронной инциализации, которую нельзя вызвать в конструкторе
    {
        private readonly WebApplicationFactory<Program> _app;
        private readonly IServiceScope _scope;
        private readonly HttpClient _client;
        private readonly CakeCrafterDbContext _dbContext;
        private readonly Fixture _fixture;
        public CakesControllerTests(WebApplicationFactory<Program> app)
        {
            _app = app.WithWebHostBuilder(webHostBuilder =>         // Есть подозрение, что эту настройку надо вынести в кастомную Factory, чтобы эта лямбда не вызывалась каждый раз для каждого метода теста
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
        public async Task Get_ShouldReturnOKStatusCodeAndItemsPage()
        {
            await CreateTestCakeEntity();
            var response = await _client.GetAsync("api/cakes?categoryId=1&skip=0&take=5");

            var content = await response.Content.ReadAsStringAsync();
            var cakesResponse = JsonConvert.DeserializeObject<ItemsPage<CakeGetResponse>>(content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.NotNull(cakesResponse);
            Assert.NotNull(cakesResponse.Items);
            Assert.IsType<ItemsPage<CakeGetResponse>>(cakesResponse);
            Assert.True(cakesResponse.Items.All(item => item.GetType() == typeof(CakeGetResponse)));
        }

        [Fact]
        public async Task GetById_ShouldReurnOKStatusCodeAndCakeGetResponse()
        {
            int cakeId = await CreateTestCakeEntity();
            var response = await _client.GetAsync($"api/cakes/{cakeId}");

            var content = await response.Content.ReadAsStringAsync();
            var cakeResponse = JsonConvert.DeserializeObject<CakeGetResponse>(content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.IsType<CakeGetResponse>(cakeResponse);
        }

        [Fact]
        public async Task Create_ShouldReturnOKStatusCodeAndCreateCakeAndReturnIntId()
        {
            var cakeCreateRequest = _fixture.Build<CakeCreateRequest>()
                                        .Without(x => x.TasteId)
                                        .Without(x => x.CategoryId)
                                        .Without(x => x.ImageId)
                                        .Create();

            var jsonCakeCreateRequest = JsonConvert.SerializeObject(cakeCreateRequest);
            var requestContent = new StringContent(jsonCakeCreateRequest, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/cakes", requestContent);

            var responseContent = await response.Content.ReadAsStringAsync();
            _ = int.TryParse(responseContent, out int cakeId);

            Assert.True(response.IsSuccessStatusCode);
            Assert.IsType<int>(cakeId);
        }

        [Fact]
        public async Task Update_ShuoldReturnOKStatusCodeAndCakeGetResponse()
        {
            var cakeId = await CreateTestCakeEntity();
            var cakeUpdateRequest = _fixture.Build<CakeUpdateRequest>()
                                        .Without(x => x.TasteId)
                                        .Without(x => x.CategoryId)
                                        .Without(x => x.ImageId)
                                        .Create();

            var jsonCakeUpdateRequest = JsonConvert.SerializeObject(cakeUpdateRequest);
            var requestContent = new StringContent(jsonCakeUpdateRequest, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync($"api/cakes/{cakeId}", requestContent);

            var responseContent = await response.Content.ReadAsStringAsync();
            var cakeGetResponse = JsonConvert.DeserializeObject<CakeGetResponse>(responseContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.IsType<CakeGetResponse>(cakeGetResponse);
        }

        [Fact]
        public async Task Delete_ShouldReturnOKStatusCode()
        {
            var cakeId = await CreateTestCakeEntity();
            var response = await _client.DeleteAsync($"api/cakes/{cakeId}");

            Assert.True(response.IsSuccessStatusCode);
        }

        public async Task<int> CreateTestCakeEntity()
        {
            var testCakeEntity = _fixture.Build<CakeEntity>()
                                    .Without(x => x.Id)
                                    .Without(x => x.CategoryId)
                                    .Without(x => x.Category)
                                    .Without(x => x.TasteId)
                                    .Without(x => x.Taste)
                                    .Without(x => x.ImageId)
                                    .Without(x => x.Image)
                                    .Create();

            await _dbContext.Cakes.AddAsync(testCakeEntity);
            await _dbContext.SaveChangesAsync();
            _dbContext.ChangeTracker.Clear();
            return testCakeEntity.Id;
        }
    }
}