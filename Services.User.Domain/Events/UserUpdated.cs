namespace Services.User.Domain.Events
{
    public class UserUpdated : IDomainEvent
    {
        public UserUpdated(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; }
        public string Name { get; }
    }
}
