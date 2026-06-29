using TechnicalTestOpea.Core.Application.Queries.Loans.Models;

namespace TechnicalTestOpea.Core.Application.Abstractions.ReadRepositories
{
    public interface ILoanReadRepository
    {
        Task<IReadOnlyCollection<LoanResponse>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
