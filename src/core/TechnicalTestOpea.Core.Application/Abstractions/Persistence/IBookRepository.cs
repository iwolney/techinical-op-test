using TechnicalTestOpea.Core.Domain.Entities;

namespace TechnicalTestOpea.Core.Application.Abstractions.Persistence
{
    public interface IBookRepository
    {
        Task AddAsync(Book book, CancellationToken cancellationToken = default);

        Task<Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        void Update(Book book);
    }
}
