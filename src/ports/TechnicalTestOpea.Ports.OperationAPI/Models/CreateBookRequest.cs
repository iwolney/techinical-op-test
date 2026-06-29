namespace TechnicalTestOpea.Ports.OperationAPI.Models
{
    public sealed record CreateBookRequest(
        string Title,
        string Author,
        int PublicationYear,
        int QuantityAvailable);
}
