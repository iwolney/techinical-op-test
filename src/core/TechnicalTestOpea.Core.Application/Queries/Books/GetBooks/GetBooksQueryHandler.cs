using MediatR;
using TechnicalTestOpea.Core.Application.Abstractions.ReadRepositories;
using TechnicalTestOpea.Core.Application.Queries.Books.GetBooks;
using TechnicalTestOpea.Core.Application.Queries.Books.Models;

namespace TechnicalTestOpea.Core.Application.Books.Queries.GetBooks;

public sealed class GetBooksQueryHandler (IBookReadRepository repository) : IRequestHandler<GetBooksQuery, IReadOnlyCollection<BookResponse>>
{    
    public async Task<IReadOnlyCollection<BookResponse>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetAllAsync(cancellationToken);
    }
}