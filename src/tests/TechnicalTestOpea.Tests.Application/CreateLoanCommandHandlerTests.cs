using FluentAssertions;
using Moq;
using TechnicalTestOpea.Core.Application.Abstractions.Persistence;
using TechnicalTestOpea.Core.Application.Features.Loans.Commands.CreateLoan;
using TechnicalTestOpea.Core.Domain.Common;
using TechnicalTestOpea.Core.Domain.Entities;

namespace TechnicalTestOpea.Tests.Application;

public class CreateLoanCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldCreateLoan_WhenBookExists()
    {
        var book = Book.Create("Livro", "Autor", 2024, 2);

        var bookRepository = new Mock<IBookRepository>();
        var loanRepository = new Mock<ILoanRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        bookRepository
            .Setup(x => x.GetByIdAsync(book.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(book);

        unitOfWork
            .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var handler = new CreateLoanCommandHandler(
            bookRepository.Object,
            loanRepository.Object,
            unitOfWork.Object);

        var command = new CreateLoanCommand(book.Id, "João");

        var loanId = await handler.Handle(command, CancellationToken.None);

        loanId.Should().NotBeEmpty();
        book.QuantityAvailable.Should().Be(1);

        loanRepository.Verify(
            x => x.AddAsync(
                It.Is<Loan>(l =>
                    l.Id == loanId &&
                    l.BookId == book.Id &&
                    l.BorrowerName == "João"),
                It.IsAny<CancellationToken>()),
            Times.Once);

        bookRepository.Verify(
            x => x.Update(book),
            Times.Once);

        unitOfWork.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrow_WhenBookDoesNotExist()
    {
        var bookRepository = new Mock<IBookRepository>();
        var loanRepository = new Mock<ILoanRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        bookRepository
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Book?)null);

        var handler = new CreateLoanCommandHandler(
            bookRepository.Object,
            loanRepository.Object,
            unitOfWork.Object);

        var command = new CreateLoanCommand(Guid.NewGuid(), "João");

        var act = async () => await handler.Handle(command, CancellationToken.None);

        await act.Should()
            .ThrowAsync<DomainException>()
            .WithMessage("Livro não encontrado.");
    }
}