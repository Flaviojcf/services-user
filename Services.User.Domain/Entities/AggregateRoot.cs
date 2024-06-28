
using Services.User.Domain.Events;

namespace Services.User.Domain.Entities
{
    public class AggregateRoot : IEntityBase
    {
        public List<IDomainEvent> _events = new List<IDomainEvent>();
        public Guid Id { get; protected set; }

        protected void AddEvent(IDomainEvent @event)
        {
            if (_events == null)
                _events = new List<IDomainEvent>();

            _events.Add(@event);
        }

        public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => _events.AsReadOnly();
    }
}
