using static TechnicalTestOpea.Core.Domain.Events.DomainEvents;

namespace TechnicalTestOpea.Core.Domain.Events
{
    public record BookCreatedDomainEvent(
        Guid BookId,
        string Title,
        string Author,
        int PublicationYear,
        int QuantityAvailable) 
        : DomainEvent;
}
