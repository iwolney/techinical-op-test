using FluentAssertions;
using TechnicalTestOpea.Core.Domain.Common;
using TechnicalTestOpea.Core.Domain.Entities;
using TechnicalTestOpea.Core.Domain.Enums;

namespace TechnicalTestOpea.Tests.Domain
{
    public class LoanTests
    {
        [Fact]
        public void Create_ShouldCreateActiveLoan_AndDecreaseBookQuantity()
        {
            var book = Book.Create("Livro", "Autor", 2024, 2);

            var loan = Loan.Create(book, "João");

            loan.Id.Should().NotBeEmpty();
            loan.BookId.Should().Be(book.Id);
            loan.BorrowerName.Should().Be("João");
            loan.Status.Should().Be(LoanStatus.Active);
            book.QuantityAvailable.Should().Be(1);
        }

        [Fact]
        public void Create_ShouldThrow_WhenBookHasNoQuantity()
        {
            var book = Book.Create("Livro", "Autor", 2024, 0);

            var act = () => Loan.Create(book, "João");

            act.Should()
                .Throw<DomainException>()
                .WithMessage("Livro indisponível!");
        }

        [Fact]
        public void Create_ShouldThrow_WhenBorrowerNameIsEmpty()
        {
            var book = Book.Create("Livro", "Autor", 2024, 1);

            var act = () => Loan.Create(book, "");

            act.Should()
                .Throw<DomainException>()
                .WithMessage("Nome do leitor é obrigatório.");
        }

        [Fact]
        public void Return_ShouldChangeStatus_AndIncreaseBookQuantity()
        {
            var book = Book.Create("Livro", "Autor", 2024, 1);
            var loan = Loan.Create(book, "João");

            loan.Return(book);

            loan.Status.Should().Be(LoanStatus.Returned);
            loan.ReturnDate.Should().NotBeNull();
            book.QuantityAvailable.Should().Be(1);
        }

        [Fact]
        public void Return_ShouldThrow_WhenLoanAlreadyReturned()
        {
            var book = Book.Create("Livro", "Autor", 2024, 1);
            var loan = Loan.Create(book, "João");

            loan.Return(book);

            var act = () => loan.Return(book);

            act.Should()
                .Throw<DomainException>()
                .WithMessage("O empréstimo já foi devolvido.");
        }
    }
}
