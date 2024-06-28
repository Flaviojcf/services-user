using MediatR;

namespace Services.User.Application.Commands.UpdateUser
{
    public class UpdateUserCommand(Guid id, string name, string password) : IRequest<Unit>
    {
        public Guid Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string Password { get; set; } = password;
    }
}
