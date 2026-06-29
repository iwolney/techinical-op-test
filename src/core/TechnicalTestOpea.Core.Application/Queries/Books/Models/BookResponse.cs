namespace TechnicalTestOpea.Core.Application.Queries.Books.Models
{
    public sealed record BookResponse(
    Guid Id,
    string Title,
    string Author,
    int PublicationYear,
    int QuantityAvailable);
}
