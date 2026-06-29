using Microsoft.EntityFrameworkCore;
using TechnicalTestOpea.Adapters.Infraestructure.RelationalDataBase.Context;
using TechnicalTestOpea.Core.Application.Abstractions.Persistence;
using TechnicalTestOpea.Core.Domain.Entities;

namespace TechnicalTestOpea.Adapters.Infraestructure.RelationalDataBase.Repositories
{
    public class LoanRepository(LibraryDbContext context) : ILoanRepository
    {
        public async Task AddAsync(Loan loan, CancellationToken cancellationToken = default)
        {
            await context.Loans.AddAsync(loan, cancellationToken);
        }

        public async Task<Loan?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await context.Loans.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public void Update(Loan loan)
        {
            context.Loans.Update(loan);
        }
    }
}
