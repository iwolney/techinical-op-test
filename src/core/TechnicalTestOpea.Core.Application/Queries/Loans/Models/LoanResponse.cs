namespace TechnicalTestOpea.Core.Application.Queries.Loans.Models
{
    public sealed record LoanResponse(
        Guid LoanId,
        Guid BookId,
        string BookTitle,
        string BorrowerName,
        DateTime LoanDate,
        DateTime? ReturnDate,
        string Status);
}
