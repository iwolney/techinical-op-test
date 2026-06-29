using MediatR;

namespace TechnicalTestOpea.Core.Application.Features.Loans.Commands.ReturnLoan
{
    public sealed record ReturnLoanCommand(Guid LoanId) : IRequest;
}
