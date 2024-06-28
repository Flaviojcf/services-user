using MediatR;

namespace Services.User.Application.Commands.DeleteUser
{
    public class DeleteUserCommand(Guid id) : IRequest<Unit>
    {
        public Guid Id { get; set; } = id;
    }
}
