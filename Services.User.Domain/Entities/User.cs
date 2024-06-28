using Services.User.Domain.Events;
using Services.User.Domain.Exceptions;

namespace Services.User.Domain.Entities
{
    public class User : AggregateRoot
    {
        public User(string name, string email, string password)
        {
            ValidateCreateDomain(name, email, password);

            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Password = password;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            IsActive = true;

            AddEvent(new UserCreated(Id, Name, Email));
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public bool IsActive { get; private set; }

        public void Delete()
        {
            IsActive = false;
            UpdatedAt = DateTime.Now;
        }

        public void Update(string name, string password)
        {
            ValidateUpdateDomain(name, password);

            Name = name;
            Password = password;
            UpdatedAt = DateTime.Now;

            AddEvent(new UserUpdated(Id, Name));
        }


        private static void ValidateCreateDomain(string name, string email, string password)
        {
            DomainException.When(string.IsNullOrEmpty(name), "Name is required");
            DomainException.When(string.IsNullOrEmpty(email), "Email is required");
            DomainException.When(string.IsNullOrEmpty(password), "Password is required");
        }

        private static void ValidateUpdateDomain(string name, string password)
        {
            DomainException.When(string.IsNullOrEmpty(name), "Name is required");
            DomainException.When(string.IsNullOrEmpty(password), "Password is required");
        }
    }
}
