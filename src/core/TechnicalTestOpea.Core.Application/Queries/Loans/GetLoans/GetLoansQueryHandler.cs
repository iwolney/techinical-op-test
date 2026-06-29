using MediatR;
using TechnicalTestOpea.Core.Application.Abstractions.ReadRepositories;
using TechnicalTestOpea.Core.Application.Queries.Loans.Models;

namespace TechnicalTestOpea.Core.Application.Queries.Loans.GetLoans
{
    public sealed class GetLoansQueryHandler(ILoanReadRepository repository) : IRequestHandler<GetLoansQuery, IReadOnlyCollection<LoanResponse>>
    {
        public async Task<IReadOnlyCollection<LoanResponse>> Handle(GetLoansQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetAllAsync(cancellationToken);
        }
    }
}
