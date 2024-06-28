using MediatR;
using Services.User.Domain.Repositories;

namespace Services.User.Application.Commands.DeleteUser
{
    public class DeleteUserCommandHandler(IUserRepository userRepository) : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IUserRepository _userRepository = userRepository;
        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _userRepository.DeleteAsync(request.Id);

            return Unit.Value;
        }
    }
}
