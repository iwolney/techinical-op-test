namespace TechnicalTestOpea.Contracts.Events
{
        public record BookCreatedIntegrationEvent(
        Guid BookId,
        string Title,
        string Author,
        int PublicationYear,
        int QuantityAvailable);
}
