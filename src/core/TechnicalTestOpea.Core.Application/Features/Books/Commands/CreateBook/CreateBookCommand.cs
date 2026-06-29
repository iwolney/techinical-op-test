using MediatR;

namespace TechnicalTestOpea.Core.Application.Features.Books.Commands.CreateBook
{
    public sealed record CreateBookCommand(
        string Title,
        string Author,
        int PublicationYear,
        int QuantityAvailable) : IRequest<Guid>;
}
