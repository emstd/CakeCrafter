using CakeCrafter.Core.Models;
using System.Net.Mime;

namespace CakeCrafter.IntegrationTests.Tests
{
    public class CategoriesControllerTests : BaseInitialization, IClassFixture<WebApplicationFactory<Program>>
    {
        public CategoriesControllerTests(WebApplicationFactory<Program> app) : base(app)
        {
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
    }
}
