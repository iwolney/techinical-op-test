using MediatR;
using TechnicalTestOpea.Core.Application.Queries.Loans.Models;

namespace TechnicalTestOpea.Core.Application.Queries.Loans.GetLoans
{
    public sealed record GetLoansQuery() : IRequest<IReadOnlyCollection<LoanResponse>>;
}
