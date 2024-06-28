using MediatR;

namespace Services.User.Application.Commands.CreateUser
{
    public class CreateUserCommand(string name, string email, string password) : IRequest<Guid>
    {
        public string Name { get; set; } = name;
        public string Email { get; set; } = email;
        public string Password { get; set; } = password;
    }
}
