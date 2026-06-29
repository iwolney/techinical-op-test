using TechnicalTestOpea.Core.Domain.Entities;

namespace TechnicalTestOpea.Core.Application.Abstractions.Persistence
{
    public interface ILoanRepository
    {
        Task AddAsync(Loan loan, CancellationToken cancellationToken = default);

        Task<Loan?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        void Update(Loan loan);
    }
}
