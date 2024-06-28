using MediatR;
using Services.User.Domain.Repositories;
using Services.User.Domain.Services;

namespace Services.User.Application.Commands.DeleteUser
{
    public class DeleteUserCommandHandler(IUserRepository userRepository, IUserService userService) : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IUserService _userService = userService;
        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserById(request.Id);

            user.Delete();

            await _userRepository.UpdateAsync(user);

            return Unit.Value;
        }
    }
}
