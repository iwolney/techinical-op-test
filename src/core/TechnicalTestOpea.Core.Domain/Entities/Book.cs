using TechnicalTestOpea.Core.Domain.Common;
using TechnicalTestOpea.Core.Domain.Events;

namespace TechnicalTestOpea.Core.Domain.Entities
{
    public class Book : AggregateRoot
    {
        public Guid Id { get; private set; }
        public string? Title { get; private set; }
        public string? Author { get; private set; }
        public int PublicationYear { get; private set; }
        public int QuantityAvailable { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private Book() { }

        public static Book Create(string title, string author, int publicationYear, int quantityAvailable)
        {

            if (string.IsNullOrWhiteSpace(title))
                throw new DomainException("Título do livro é obrigatório!");

            if(string.IsNullOrWhiteSpace(author))
                throw new DomainException("Informe o nome do autor do livro!");

            if (quantityAvailable < 0)
                throw new DomainException("Quantidade não pode ser negativa.");

            var book = new Book
            {
                Id = Guid.NewGuid(),
                Title = title,
                Author = author,
                PublicationYear = publicationYear,
                QuantityAvailable = quantityAvailable
            };


            book.RaiseDomainEvent(new BookCreatedDomainEvent(
                book.Id,
                book.Title,
                book.Author,
                book.PublicationYear,
                book.QuantityAvailable));

            return book;
        }

        public void Borrow()
        {
            if (QuantityAvailable <= 0)
                throw new DomainException("Livro indisponível!");

            QuantityAvailable--;
        }

        public void Return()
        {
            QuantityAvailable++;
        }
    }
}
