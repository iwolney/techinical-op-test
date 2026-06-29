using MediatR;

namespace TechnicalTestOpea.Core.Domain.Events
{
    public abstract class DomainEvents
    {
        public abstract record DomainEvent : INotification;
    }
}
