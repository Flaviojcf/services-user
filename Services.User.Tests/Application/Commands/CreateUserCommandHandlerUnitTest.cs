using Moq;
using Services.User.Application.Commands.CreateUser;
using Services.User.Domain.Repositories;
using UserEntity = Services.User.Domain.Entities.User;

namespace Services.Tests.Application.Commands
{
    [Collection(nameof(CreateUserCommandHandler))]
    public class CreateUserCommandHandlerUnitTest
    {
        [Fact]
        [Trait("Application", "CreateUser - Command")]
        public async Task InputDataIsOk_Executed_CreateUser()
        {
            var userRepository = new Mock<IUserRepository>();

            var createUserCommand = new CreateUserCommand("John Doe", "john.doe@example.com", "p@ssw0rd");

            var createUserCommandHandler = new CreateUserCommandHandler(userRepository.Object);

            // Act
            await createUserCommandHandler.Handle(createUserCommand, new CancellationToken());

            // Assert
            userRepository.Verify(u => u.AddAsync(It.IsAny<UserEntity>()), Times.Once);
        }
    }
}
