using static TechnicalTestOpea.Core.Domain.Events.DomainEvents;

namespace TechnicalTestOpea.Core.Domain.Common
{
    public abstract class AggregateRoot
    {
        private readonly List<DomainEvent> events = [];

        public IReadOnlyCollection<DomainEvent> Events 
            => events.AsReadOnly();

        protected void RaiseDomainEvent(DomainEvent @event) 
            => events.Add(@event);

        public void ClearDomainEvents() 
            => events.Clear();
    }
}
