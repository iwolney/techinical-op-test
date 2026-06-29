using MediatR;
using TechnicalTestOpea.Core.Application.Queries.Books.Models;

namespace TechnicalTestOpea.Core.Application.Queries.Books.GetBooks
{
    public sealed record GetBooksQuery() : IRequest<IReadOnlyCollection<BookResponse>>;
}
