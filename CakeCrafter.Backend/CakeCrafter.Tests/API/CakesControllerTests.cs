using AutoMapper;
using CakeCrafter.API.Controllers;
using CakeCrafter.API.Options;
using CakeCrafter.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;

namespace CakeCrafter.Tests.API
{
    public class CakesControllerTests
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task GetById_ShouldReturnNotFoundAsync(int id)
        {
            //Arrange
            var service = new Mock<ICakeService>();
            var mapper = new Mock<IMapper>();
            var settings = new Mock<IOptions<ImageHostSettings>>();

            CakesController cakesController = new CakesController(service.Object, mapper.Object, settings.Object);

            //Act
            var result = await cakesController.GetCakeById(id);

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
