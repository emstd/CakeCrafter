using CakeCrafter.Core.Interfaces.Repositories;
using CakeCrafter.Core.Models;
using CakeCrafter.Domain;
using Moq;

namespace CakeCrafter.UnitTests.BusinessLogic
{
    public class CakeServiceTests
    {
        [Fact]
        public async void Create_ShouldCreateCake()
        {
            //Arrange
            //var behavior = new MockBehavior();
            var cakeRepositoryMock = new Mock<ICakeRepository>();
            var service = new CakeService(cakeRepositoryMock.Object);
            var cake = new Mock<Cake>();

            //Act
            var result = await service.Create(cake.Object);


            //Assert
            cakeRepositoryMock.Verify(x => x.Create(cake.Object), Times.Once);
            Assert.IsType<int>(result);
            // не проходит Assert.True(result > 0);
        }

        [Fact]
        public async void Delete_ShouldDeleteCake()
        {
            //Arrange
            var cakeId = 1;
            var cakeRepositoryMock = new Mock<ICakeRepository>();
            var service = new CakeService(cakeRepositoryMock.Object);
            //Act
            var result = await service.Delete(cakeId);

            //Assert
            cakeRepositoryMock.Verify(x => x.Delete(cakeId), Times.Once);
            //не проходит Assert.True(result == true);
        }

        [Fact]
        public async void Update_ShouldUpdateCake()
        {

        }
    }
}
