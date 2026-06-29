using MongoDB.Driver;
using TechnicalTestOpea.Adapters.Infraestructure.NoSQL.ReadModels;
using TechnicalTestOpea.Core.Application.Abstractions.ReadRepositories;
using TechnicalTestOpea.Core.Application.Queries.Loans.Models;

namespace TechnicalTestOpea.Adapters.Infraestructure.NoSQL.Repositories
{
    public sealed class LoanReadRepository(IMongoDatabase database) : ILoanReadRepository
    {
        private readonly IMongoCollection<BookReadModel> _books = database.GetCollection<BookReadModel>("books");

        public async Task<IReadOnlyCollection<LoanResponse>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var books = await _books
                .Find(_ => true)
                .ToListAsync(cancellationToken);

            var activeLoans = books.SelectMany(book =>
                book.ActiveLoans.Select(loan =>
                    new LoanResponse(
                        loan.LoanId,
                        book.Id,
                        book.Title,
                        loan.BorrowerName,
                        loan.LoanDate,
                        loan.ReturnDate,
                        loan.Status)));

            var historyLoans = books.SelectMany(book =>
                book.LoanHistory.Select(loan =>
                    new LoanResponse(
                        loan.LoanId,
                        book.Id,
                        book.Title,
                        loan.BorrowerName,
                        loan.LoanDate,
                        loan.ReturnDate,
                        loan.Status)));

            return activeLoans
                .Concat(historyLoans)
                .OrderByDescending(x => x.LoanDate)
                .ToList();
        }
    }
}
