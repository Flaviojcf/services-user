using Moq;
using Services.User.Application.Commands.UpdateUser;
using Services.User.Domain.Repositories;
using Services.User.Domain.Services;
using UserEntity = Services.User.Domain.Entities.User;

namespace Services.Tests.Application.Commands
{
    [Collection(nameof(UpdateUserCommandHandler))]
    public class UpdateUserCommandHandlerUnitTest
    {

        [Fact]
        [Trait("Application", "UpdateUser - Command")]
        public async Task InputDataIsOk_Executed_UpdateUser()
        {
            // Arrange
            var user = new UserEntity("John Doe", "john.doe@example.com", "p@ssw0rd");
            var userRepository = new Mock<IUserRepository>();
            var userService = new Mock<IUserService>();
            userService.Setup(s => s.GetUserById(user.Id)).ReturnsAsync(user);
            var updateUserCommand = new UpdateUserCommand(user.Id, user.Name, user.Password);

            var updateUserCommandHandler = new UpdateUserCommandHandler(userRepository.Object, userService.Object);


            // Act
            await updateUserCommandHandler.Handle(updateUserCommand, new CancellationToken());



            // Assert
            userRepository.Verify(u => u.UpdateAsync(user), Times.Once());
        }

    }
}
