using CakeCrafter.Core.Pages;

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
        public async Task Get_ShouldReturnOKStatusCodeAndItemsPage()
        {
            var response = await _client.GetAsync("api/cakes?categoryId=1&skip=0&take=5");

            var content = await response.Content.ReadAsStringAsync();
            var cakesResponse = JsonConvert.DeserializeObject<ItemsPage<CakeGetResponse>>(content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.NotNull(cakesResponse);
            Assert.NotNull(cakesResponse.Items);
            Assert.IsType<ItemsPage<CakeGetResponse>> (cakesResponse);
            Assert.True(cakesResponse.Items.All(item => item.GetType() == typeof(CakeGetResponse)));
        }

        [Fact]
        public async Task GetById_ShouldReurnOKStatusCodeAndCakeGetResponse()
        {
            //если базу все-таки чистить, то нужно добавить логику предварительного создания торта
            var response = await _client.GetAsync("api/cakes/4");

            var content = await response.Content.ReadAsStringAsync();
            var cakeResponse = JsonConvert.DeserializeObject<CakeGetResponse>(content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.IsType<CakeGetResponse>(cakeResponse);
        }

        [Fact]
        public async Task Create_ShouldReturnOKStatusCodeAndCreateCakeAndReturnIntId()
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
            int id = 5;                                         //Перед тестом или чистить базу и добавлять новый торт заранее, затем менять или получать Id уже существующих в тестовой базе?
            var cakeUpdateRequest = new CakeUpdateRequest()
            {
                Name = "Измененный в тесте торт",
                Description = "Вкусный тестовый торт",
                //ImageId = Guid.NewGuid(),             //чтобы отправить с GUID'ой, надо уже иметь существующую в базе из-за внешнего ключа
                CookTimeInMinutes = 90,
                TasteId = null,
                CategoryId = null,
                Level = 2,
                Weight = 1
            };

            var jsonCakeUpdateRequest = JsonConvert.SerializeObject(cakeUpdateRequest);
            var requestContent = new StringContent(jsonCakeUpdateRequest, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync($"api/cakes/{id}", requestContent);

            var responseContent = await response.Content.ReadAsStringAsync();
            var cakeGetResponse = JsonConvert.DeserializeObject<CakeGetResponse>(responseContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.IsType<CakeGetResponse>(cakeGetResponse);
        }

        [Fact]
        public async Task Delete_ShouldReturnOKStatusCode()
        {
            var response = await _client.DeleteAsync("api/cakes/3");    //Опять же получается, что тест пройдет только один раз, если не чистить базу и не создавать заранее удаляемый Cake.
                                                                        //Еще вдобавок сначала отработал этот тест, а потом GetById с тем же Id и соответственно GetById не прошел
            Assert.True(response.IsSuccessStatusCode);
        }
    }
}
