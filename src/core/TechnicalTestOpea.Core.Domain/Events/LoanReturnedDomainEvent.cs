using static TechnicalTestOpea.Core.Domain.Events.DomainEvents;

namespace TechnicalTestOpea.Core.Domain.Events
{
    public sealed record LoanReturnedDomainEvent(
        Guid LoanId,
        Guid BookId,
        DateTime ReturnDate,
        string Status,
        int QuantityAvailable) : DomainEvent;
}
