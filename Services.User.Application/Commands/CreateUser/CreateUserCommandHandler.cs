using MediatR;
using Services.User.Domain.Repositories;
using UserEntity = Services.User.Domain.Entities.User;

namespace Services.User.Application.Commands.CreateUser
{
    public class CreateUserCommandHandler(IUserRepository userRepository) : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository = userRepository;
        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new UserEntity(request.Name, request.Email, request.Password);

            await _userRepository.AddAsync(user);

            return user.Id;
        }
    }
}
