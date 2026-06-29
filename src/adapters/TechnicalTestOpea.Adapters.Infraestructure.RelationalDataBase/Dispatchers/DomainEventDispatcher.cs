using MediatR;
using static TechnicalTestOpea.Core.Domain.Events.DomainEvents;

namespace TechnicalTestOpea.Adapters.Infraestructure.RelationalDataBase.Dispatchers
{
    public class DomainEventDispatcher(IPublisher publisher) : IDomainEventDispatcher
    {
        public async Task DispatchAsync(IReadOnlyCollection<DomainEvent> domainEvents, CancellationToken cancellationToken = default)
        {
            foreach (var domainEvent in domainEvents)
            {
                await publisher.Publish(domainEvent, cancellationToken);
            }
        }
    }
}
