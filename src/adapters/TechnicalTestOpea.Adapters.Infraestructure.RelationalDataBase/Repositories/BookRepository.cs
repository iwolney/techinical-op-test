using Microsoft.EntityFrameworkCore;
using TechnicalTestOpea.Adapters.Infraestructure.RelationalDataBase.Context;
using TechnicalTestOpea.Core.Application.Abstractions.Persistence;
using TechnicalTestOpea.Core.Domain.Entities;

namespace TechnicalTestOpea.Adapters.Infraestructure.RelationalDataBase.Repositories
{
    public class BookRepository(LibraryDbContext context) : IBookRepository
    {
        public async Task AddAsync(Book book, CancellationToken cancellationToken = default)
        {
            await context.Books.AddAsync(book, cancellationToken);
        }

        public async Task<Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await context.Books.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public void Update(Book book)
        {
            context.Books.Update(book);
        }
    }
}
