using static TechnicalTestOpea.Core.Domain.Events.DomainEvents;

namespace TechnicalTestOpea.Core.Domain.Events
{
    public sealed record LoanCreatedDomainEvent(
        Guid LoanId,
        Guid BookId,
        string BorrowerName,
        DateTime LoanDate,
        string Status,
        int QuantityAvailable) : DomainEvent;
}
