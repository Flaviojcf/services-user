using Moq;
using Services.User.Application.Commands.DeleteUser;
using Services.User.Domain.Repositories;

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
            var userRepository = new Mock<IUserRepository>();
            var id = Guid.NewGuid();
            var deleteUserCommand = new DeleteUserCommand(id);

            var deleteUserCommandHandler = new DeleteUserCommandHandler(userRepository.Object);

            // Act
            await deleteUserCommandHandler.Handle(deleteUserCommand, new CancellationToken());

            // Assert
            userRepository.Verify(u => u.DeleteAsync(id), Times.Once);
        }
    }
}
