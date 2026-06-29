using TechnicalTestOpea.Adapters.Infraestructure.RelationalDataBase.Context;
using TechnicalTestOpea.Adapters.Infraestructure.RelationalDataBase.Dispatchers;
using TechnicalTestOpea.Core.Application.Abstractions.Persistence;
using TechnicalTestOpea.Core.Domain.Common;

namespace TechnicalTestOpea.Adapters.Infraestructure.RelationalDataBase.UnitOfWork;

public class UnitOfWork(LibraryDbContext context, IDomainEventDispatcher domainEventDispatcher) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var aggregates = context.ChangeTracker
            .Entries<AggregateRoot>()
            .Select(x => x.Entity)
            .Where(x => x.Events.Any())
            .ToList();

        var domainEvents = aggregates
            .SelectMany(x => x.Events)
            .ToList();

        var result = await context.SaveChangesAsync(cancellationToken);

        if (domainEvents.Any())
        {
            await domainEventDispatcher.DispatchAsync(
                domainEvents,
                cancellationToken);

            aggregates.ForEach(x => x.ClearDomainEvents());
        }

        return result;
    }
}
