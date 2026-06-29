using MediatR;

namespace TechnicalTestOpea.Core.Application.Features.Loans.Commands.CreateLoan
{
    public sealed record CreateLoanCommand(
        Guid BookId,
        string BorrowerName) : IRequest<Guid>;
}
