namespace TechnicalTestOpea.Contracts.Events
{
    public sealed record LoanCreatedIntegrationEvent(
        Guid LoanId,
        Guid BookId,
        string BorrowerName,
        DateTime LoanDate,
        string Status,
        int QuantityAvailable);
}
