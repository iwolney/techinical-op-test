namespace TechnicalTestOpea.Contracts.Events
{
    public sealed record LoanReturnedIntegrationEvent(
        Guid LoanId,
        Guid BookId,
        DateTime ReturnDate,
        string Status,
        int QuantityAvailable);
}
