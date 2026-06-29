using MediatR;
using TechnicalTestOpea.Core.Application.Queries.Books.Models;

namespace TechnicalTestOpea.Core.Application.Queries.Books.GetBookById
{
    public sealed record GetBookByIdQuery(Guid Id) : IRequest<BookResponse?>;
}
