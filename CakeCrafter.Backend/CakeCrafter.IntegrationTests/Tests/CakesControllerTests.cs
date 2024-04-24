using CakeCrafter.Core.Pages;
using System.Net.Mime;

namespace CakeCrafter.IntegrationTests.Tests
{
    public class CakesControllerTests : BaseInitialization, IClassFixture<WebApplicationFactory<Program>>
    {
        public CakesControllerTests(WebApplicationFactory<Program> app) : base(app)
        {
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
            var requestContent = new StringContent(jsonCakeCreateRequest, Encoding.UTF8, MediaTypeNames.Application.Json);

            var response = await _client.PostAsync("api/cakes", requestContent);

            var responseContent = await response.Content.ReadAsStringAsync();
            _ = int.TryParse(responseContent, out int cakeId);

            Assert.True(response.IsSuccessStatusCode);
            Assert.IsType<int>(cakeId);
        }

        [Fact]
        public async Task Update_ShouldReturnOKStatusCodeAndCakeGetResponse()
        {
            var cakeId = await CreateTestCakeEntity();
            var cakeUpdateRequest = _fixture.Build<CakeUpdateRequest>()
                                        .Without(x => x.TasteId)
                                        .Without(x => x.CategoryId)
                                        .Without(x => x.ImageId)
                                        .Create();

            var jsonCakeUpdateRequest = JsonConvert.SerializeObject(cakeUpdateRequest);
            var requestContent = new StringContent(jsonCakeUpdateRequest, Encoding.UTF8, MediaTypeNames.Application.Json);

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
    }
}