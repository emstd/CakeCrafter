namespace CakeCrafter.IntegrationTests
{
    public class UnitTest1 : IAsyncLifetime
    {
        private readonly HttpClient _client;
        private readonly IServiceScope _scope;

        //private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder().Build();

        public UnitTest1()
        {
            var app = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(x =>
                {
                    x.UseEnvironment("IntegrationTests");
                    //x.UseSetting("ConnectionStrings:DefaultConnection", _msSqlContainer.GetConnectionString());
                });
            _client = app.CreateClient();
            _scope = app.Services.CreateScope();
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
        public async Task Test1()
        {
           
            var response = await _client.GetAsync("api/cakes?categoryId=1&skip=0&take=5");


            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Test2()
        {
            var response = await _client.GetAsync("api/cakes?categoryId=1&skip=0&take=5");


            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}