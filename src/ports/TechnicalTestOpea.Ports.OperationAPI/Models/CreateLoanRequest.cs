namespace TechnicalTestOpea.Ports.OperationAPI.Models
{
    public sealed record CreateLoanRequest(
        Guid BookId,
        string BorrowerName);
}
