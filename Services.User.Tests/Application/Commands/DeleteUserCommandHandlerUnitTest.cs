using Moq;
using Services.User.Application.Commands.DeleteUser;
using Services.User.Domain.Repositories;
using Services.User.Domain.Services;
using UserEntity = Services.User.Domain.Entities.User;

namespace Services.Tests.Application.Commands
{

    [Collection(nameof(DeleteUserCommandHandler))]
    public class DeleteUserCommandHandlerUnitTest
    {
        [Fact]
        [Trait("Application", "DeleteUser - Command")]
        public async Task InputDataIsOk_Executed_DeleteUser()
        {
            // Arrange
            var user = new UserEntity("John Doe", "john.doe@example.com", "p@ssw0rd");
            var userRepository = new Mock<IUserRepository>();
            var userService = new Mock<IUserService>();
            userService.Setup(s => s.GetUserById(user.Id)).ReturnsAsync(user);

            var deleteUserCommand = new DeleteUserCommand(user.Id);

            var deleteUserCommandHandler = new DeleteUserCommandHandler(userRepository.Object, userService.Object);

            // Act
            await deleteUserCommandHandler.Handle(deleteUserCommand, new CancellationToken());

            // Assert
            userRepository.Verify(u => u.UpdateAsync(user), Times.Once);
        }
    }
}
