using MediatR;
using TechnicalTestOpea.Contracts.Events;
using TechnicalTestOpea.Core.Application.Abstractions.Messaging;
using TechnicalTestOpea.Core.Domain.Events;

namespace TechnicalTestOpea.Core.Application.EventHandlers
{
    public sealed class LoanReturnedDomainEventHandler (IIntegrationEventPublisher publisher) : INotificationHandler<LoanReturnedDomainEvent>
    {
        public async Task Handle(LoanReturnedDomainEvent notification, CancellationToken cancellationToken)
        {
            await publisher.PublishAsync(
                new LoanReturnedIntegrationEvent(
                    notification.LoanId,
                    notification.BookId,
                    notification.ReturnDate,
                    notification.Status,
                    notification.QuantityAvailable),
                cancellationToken);
        }
    }
}
