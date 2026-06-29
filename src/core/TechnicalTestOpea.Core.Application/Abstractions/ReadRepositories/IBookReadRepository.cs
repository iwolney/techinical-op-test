using TechnicalTestOpea.Core.Application.Queries.Books.Models;

namespace TechnicalTestOpea.Core.Application.Abstractions.ReadRepositories
{
    public interface IBookReadRepository
    {
        Task<BookResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IReadOnlyCollection<BookResponse>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
