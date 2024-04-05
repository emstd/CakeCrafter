namespace CakeCrafter.IntegrationTests.Tests
{
    public class CakesControllerTests : IClassFixture<WebApplicationFactory<Program>>,  //этот интерфейс позволяет создать WebApplicationFactory один раз на все тестовые методы
                                        IAsyncLifetime                                  //для асинхронной инциализации, которую нельзя вызвать в конструкторе
    {
        private readonly WebApplicationFactory<Program> _app;
        private readonly IServiceScope _scope;
        private readonly HttpClient _client;
        private readonly CakeCrafterDbContext _dbContext;
        public CakesControllerTests(WebApplicationFactory<Program> app)
        {
            _app = app.WithWebHostBuilder(webHostBuilder =>         // Есть подозрение, что эту настройку надо вынести в кастомную Factory, чтобы эта лямбда не вызывалась каждый раз для каждого метода теста
            {
                webHostBuilder.UseEnvironment("IntegrationTests");
            });

            _scope = _app.Services.CreateScope();
            _client = _app.CreateClient();
            _dbContext = _scope.ServiceProvider.GetRequiredService<CakeCrafterDbContext>(); //Создал отдельно контекст не только для миграции, но и для создания required сущностей, прада у меня их нет, просто попробовать.
        }

        public Task DisposeAsync()
        {
            _scope.Dispose();
            _client.Dispose();
            return Task.CompletedTask;
        }

        public async Task InitializeAsync()
        {
            await _dbContext.Database.MigrateAsync();
            //foreach (var entity in _dbContext.Model.GetEntityTypes())         //надо ли очищать все данные из тестовой базы?
            //{
            //    await _dbContext.Database.ExecuteSqlRawAsync($"DELETE FROM [{entity.GetTableName()}]");
            //}
        }

        [Fact]
        public async Task Get_ShouldReturnOKStatusCode()
        {
            var response = await _client.GetAsync("api/cakes?categoryId=1&skip=0&take=5");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetById_ShouldReurnOKStatusCode()
        {
            //если базу все-таки чистить, то нужно добавить логику предварительного создания торта
            var response = await _client.GetAsync("api/cakes/3");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Create_ShouldCreateCakeAndReturnIntId()
        {
            var cakeCreateRequest = new CakeCreateRequest()
            {
                Name = "Наполеон",
                Description = "Вкусный сливочный торт",
                //ImageId = Guid.NewGuid(),             //чтобы отправить с GUID'ой, надо уже иметь существующую в базе из-за внешнего ключа
                CookTimeInMinutes = 90,
                TasteId = null,
                CategoryId = null,
                Level = 2,
                Weight = 1
            };

            var jsonCakeCreateRequest = JsonConvert.SerializeObject(cakeCreateRequest);

            var content = new StringContent(jsonCakeCreateRequest, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/cakes", content);

            var cakeStringId = await response.Content.ReadAsStringAsync();
            int.TryParse(cakeStringId, out int cakeIntId);


            Assert.True(response.IsSuccessStatusCode);
            Assert.IsType<int>(cakeIntId);

        }
    }
}
