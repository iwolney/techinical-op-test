using MongoDB.Driver;
using TechnicalTestOpea.Adapters.Infraestructure.NoSQL.ReadModels;
using TechnicalTestOpea.Core.Application.Abstractions.ReadRepositories;
using TechnicalTestOpea.Core.Application.Queries.Books.Models;

namespace TechnicalTestOpea.Adapters.Infraestructure.NoSQL.Repositories
{
    public class BookReadRepository(IMongoDatabase database) : IBookReadRepository
    {
        private readonly IMongoCollection<BookReadModel> collection = database.GetCollection<BookReadModel>("books");

        public async Task<IReadOnlyCollection<BookResponse>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var books = await collection
            .Find(_ => true)
            .ToListAsync(cancellationToken);

            return books
                .Select(x => new BookResponse(
                    x.Id,
                    x.Title,
                    x.Author,
                    x.PublicationYear,
                    x.QuantityAvailable))
                .ToList();
        }

        public async Task<BookResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var book = await collection.Find(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
            
            return book is null ? null : 
                new BookResponse(
                    book.Id,
                    book.Title,
                    book.Author,
                    book.PublicationYear,
                    book.QuantityAvailable);
        }
    }
}
