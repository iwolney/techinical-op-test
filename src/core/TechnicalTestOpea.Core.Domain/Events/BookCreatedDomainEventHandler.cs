using MediatR;

namespace TechnicalTestOpea.Core.Domain.Events
{
    public class BookCreatedDomainEventHandler : INotificationHandler<BookCreatedDomainEvent>
    {
        public Task Handle(BookCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
