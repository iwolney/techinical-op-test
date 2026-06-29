using TechnicalTestOpea.Core.Domain.Common;
using TechnicalTestOpea.Core.Domain.Enums;
using TechnicalTestOpea.Core.Domain.Events;

namespace TechnicalTestOpea.Core.Domain.Entities
{
    public class Loan : AggregateRoot
    {
        public Guid Id { get; private set; }
        public Guid BookId { get; private set; }
        public string? BorrowerName { get; private set; }
        public DateTime LoanDate { get; private set; }
        public DateTime? ReturnDate { get; private set; }
        public LoanStatus Status { get; private set; }

        private Loan(){ }

        public static Loan Create(Book book, string borrowerName)
        {
            if (string.IsNullOrWhiteSpace(borrowerName))
                throw new DomainException("Nome do leitor é obrigatório.");

            book.Borrow();

            var loan = new Loan
            {
                Id = Guid.NewGuid(),
                BookId = book.Id,
                BorrowerName = borrowerName,
                LoanDate = DateTime.UtcNow,
                Status = LoanStatus.Active
            };

            loan.RaiseDomainEvent(
                new LoanCreatedDomainEvent(
                    loan.Id,
                    loan.BookId,
                    loan.BorrowerName,
                    loan.LoanDate,
                    loan.Status.ToString(),
                    book.QuantityAvailable));

            return loan;
        }

        public void Return(Book book)
        {
            if (Status == LoanStatus.Returned)
                throw new DomainException("O empréstimo já foi devolvido.");

            Status = LoanStatus.Returned;
            ReturnDate = DateTime.UtcNow;

            book.Return();

            RaiseDomainEvent(
                new LoanReturnedDomainEvent(
                    Id,
                    BookId,
                    ReturnDate.Value,
                    Status.ToString(),
                    book.QuantityAvailable));
        }
    }
}
