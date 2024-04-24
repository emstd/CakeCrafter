namespace CakeCrafter.IntegrationTests
{
    public class BaseInitialization : IAsyncLifetime
    {
        protected WebApplicationFactory<Program> _app;
        protected readonly IServiceScope _scope;
        protected readonly HttpClient _client;
        protected readonly CakeCrafterDbContext _dbContext;
        protected readonly Fixture _fixture;

        public BaseInitialization(WebApplicationFactory<Program> app)
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

        public async Task DisposeAsync()
        {
            //var connection = _dbContext.Database.GetDbConnection();

            //await connection.OpenAsync();
            var respawner = await Respawner.CreateAsync("Server=(localdb)\\mssqllocaldb;database=CakeCrafterTestsDb;trusted_connection=true;TrustServerCertificate=True", new RespawnerOptions
            {
                TablesToIgnore = new Table[]
                {
                "__EFMigrationsHistory"
                }
            });

            await respawner.ResetAsync("Server=(localdb)\\mssqllocaldb;database=CakeCrafterTestsDb;trusted_connection=true;TrustServerCertificate=True");
        }

        public async Task InitializeAsync()
        {
            await _dbContext.Database.MigrateAsync();
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
