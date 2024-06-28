using MediatR;
using Services.User.Domain.Repositories;
using Services.User.Domain.Services;

namespace Services.User.Application.Commands.UpdateUser
{
    public class UpdateUserCommandHandler(IUserRepository userRepository, IUserService userService) : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IUserService _userService = userService;
        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserById(request.Id);

            user.Update(request.Name, request.Password);

            await _userRepository.UpdateAsync(user);

            return Unit.Value;
        }
    }
}
