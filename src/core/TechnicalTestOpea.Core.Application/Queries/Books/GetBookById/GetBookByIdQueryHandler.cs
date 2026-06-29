using MediatR;
using TechnicalTestOpea.Core.Application.Abstractions.ReadRepositories;
using TechnicalTestOpea.Core.Application.Queries.Books.Models;

namespace TechnicalTestOpea.Core.Application.Queries.Books.GetBookById
{
    public sealed class GetBookByIdQueryHandler(IBookReadRepository repository) : IRequestHandler<GetBookByIdQuery, BookResponse?>
    {
        public async Task<BookResponse?> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetByIdAsync(request.Id, cancellationToken);
        }
    }
}
