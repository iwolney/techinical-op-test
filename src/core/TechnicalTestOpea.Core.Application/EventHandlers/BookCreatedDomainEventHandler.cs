using MediatR;
using TechnicalTestOpea.Contracts.Events;
using TechnicalTestOpea.Core.Application.Abstractions.Messaging;
using TechnicalTestOpea.Core.Domain.Events;

namespace TechnicalTestOpea.Core.Application.EventHandlers
{
    public class BookCreatedDomainEventHandler(IIntegrationEventPublisher publisher) : INotificationHandler<BookCreatedDomainEvent>
    {
        public async Task Handle(BookCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var integrationEvent = new BookCreatedIntegrationEvent(
            notification.BookId,
            notification.Title,
            notification.Author,
            notification.PublicationYear,
            notification.QuantityAvailable);

            await publisher.PublishAsync(integrationEvent, cancellationToken);
        }
    }
}
