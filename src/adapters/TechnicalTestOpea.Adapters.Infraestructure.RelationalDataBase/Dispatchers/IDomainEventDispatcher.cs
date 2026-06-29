using static TechnicalTestOpea.Core.Domain.Events.DomainEvents;

namespace TechnicalTestOpea.Adapters.Infraestructure.RelationalDataBase.Dispatchers
{
    public interface IDomainEventDispatcher
    {
        Task DispatchAsync(IReadOnlyCollection<DomainEvent> domainEvents, CancellationToken cancellationToken = default);
    }
}
