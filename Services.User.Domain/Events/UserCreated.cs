namespace Services.User.Domain.Events
{
    public class UserCreated : IDomainEvent
    {
        public UserCreated(Guid id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Email { get; }
    }
}
