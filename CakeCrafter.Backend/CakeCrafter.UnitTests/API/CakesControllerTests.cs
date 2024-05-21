using AutoMapper;
using CakeCrafter.API.Controllers;
using CakeCrafter.API.Options;
using CakeCrafter.Core.Interfaces.Services;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace CakeCrafter.UnitTests.API
{
    public class CakesControllerTests
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task GetById_ShouldReturnNotFoundAsync(int id)
        {
            //Arrange
            var service = new Mock<ICakeService>(MockBehavior.Strict);
            var mapper = new Mock<IMapper>(MockBehavior.Strict);
            var settings = new Mock<IOptions<ImageHostSettings>>(MockBehavior.Strict);
            var logger = Mock.Of<ILogger<CakesController>>();

            CakesController cakesController = new CakesController(service.Object, mapper.Object, settings.Object, logger);

            //Act
            var result = await cakesController.GetCakeById(id);

            //Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }
    }
}
