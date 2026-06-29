using MediatR;
using TechnicalTestOpea.Contracts.Events;
using TechnicalTestOpea.Core.Application.Abstractions.Messaging;
using TechnicalTestOpea.Core.Domain.Events;

namespace TechnicalTestOpea.Core.Application.EventHandlers
{
    public sealed class LoanCreatedDomainEventHandler(IIntegrationEventPublisher publisher) : INotificationHandler<LoanCreatedDomainEvent>
    {
        public async Task Handle(LoanCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            await publisher.PublishAsync(
                new LoanCreatedIntegrationEvent(
                    notification.LoanId,
                    notification.BookId,
                    notification.BorrowerName,
                    notification.LoanDate,
                    notification.Status,
                    notification.QuantityAvailable
                    ), cancellationToken);
        }
    }
}
