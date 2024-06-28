using Moq;
using Services.User.Application.Services;
using Services.User.Domain.Services;
using UserEntity = Services.User.Domain.Entities.User;

namespace Services.Tests.Services
{
    [Collection(nameof(UserService))]
    public class UserServiceUnitTest
    {
        [Fact]
        [Trait("Application", "GetById - Service")]
        public async Task InputDataIsOk_Executed_GetUser()
        {
            // Arrange
            var userService = new Mock<IUserService>();

            var user = new UserEntity("John Doe", "john.doe@example.com", "p@ssw0rd");
            userService.Setup(s => s.GetUserById(user.Id)).ReturnsAsync(user);

            // Act
            var result = await userService.Object.GetUserById(user.Id);

            // Assert
            userService.Verify(u => u.GetUserById(user.Id), Times.Once);
            Assert.Equal(user, result);
        }

    }
}
