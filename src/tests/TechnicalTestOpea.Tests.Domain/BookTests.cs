using FluentAssertions;
using TechnicalTestOpea.Core.Domain.Common;
using TechnicalTestOpea.Core.Domain.Entities;

namespace TechnicalTestOpea.Tests.Domain
{
    public class BookTests
    {
        [Fact]
        public void Create_ShouldThrow_WhenTitleIsEmpty()
        {
            var act = () => Book.Create("", "Autor", 2024, 1);

            act.Should()
                .Throw<DomainException>()
                .WithMessage("Título do livro é obrigatório!");
        }

        [Fact]
        public void Create_ShouldThrow_WhenAuthorIsEmpty()
        {
            var act = () => Book.Create("Livro", "", 2024, 1);

            act.Should()
                .Throw<DomainException>()
                .WithMessage("Informe o nome do autor do livro!");
        }

        [Fact]
        public void Borrow_ShouldDecreaseQuantity()
        {
            var book = Book.Create("Livro", "Autor", 2024, 2);

            book.Borrow();

            book.QuantityAvailable.Should().Be(1);
        }

        [Fact]
        public void Borrow_ShouldThrow_WhenQuantityIsZero()
        {
            var book = Book.Create("Livro", "Autor", 2024, 0);

            var act = () => book.Borrow();

            act.Should()
                .Throw<DomainException>()
                .WithMessage("Livro indisponível!");
        }

        [Fact]
        public void Return_ShouldIncreaseQuantity()
        {
            var book = Book.Create("Livro", "Autor", 2024, 1);

            book.Return();

            book.QuantityAvailable.Should().Be(2);
        }
    }
}
