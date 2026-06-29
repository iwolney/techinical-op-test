using FluentAssertions;
using Moq;
using TechnicalTestOpea.Core.Application.Abstractions.Persistence;
using TechnicalTestOpea.Core.Application.Features.Loans.Commands.ReturnLoan;
using TechnicalTestOpea.Core.Domain.Common;
using TechnicalTestOpea.Core.Domain.Entities;

namespace TechnicalTestOpea.Tests.Application;

public class ReturnLoanCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnLoan()
    {
        var book = Book.Create("Livro", "Autor", 2024, 1);
        var loan = Loan.Create(book, "João");

        var bookRepository = new Mock<IBookRepository>();
        var loanRepository = new Mock<ILoanRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        loanRepository
            .Setup(x => x.GetByIdAsync(loan.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(loan);

        bookRepository
            .Setup(x => x.GetByIdAsync(book.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(book);

        unitOfWork
            .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var handler = new ReturnLoanCommandHandler(
            bookRepository.Object,
            loanRepository.Object,
            unitOfWork.Object);

        await handler.Handle(
            new ReturnLoanCommand(loan.Id),
            CancellationToken.None);

        loan.ReturnDate.Should().NotBeNull();
        book.QuantityAvailable.Should().Be(1);

        loanRepository.Verify(x => x.Update(loan), Times.Once);
        bookRepository.Verify(x => x.Update(book), Times.Once);
        unitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrow_WhenLoanDoesNotExist()
    {
        var bookRepository = new Mock<IBookRepository>();
        var loanRepository = new Mock<ILoanRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        loanRepository
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Loan?)null);

        var handler = new ReturnLoanCommandHandler(
            bookRepository.Object,
            loanRepository.Object,
            unitOfWork.Object);

        var act = async () => await handler.Handle(
            new ReturnLoanCommand(Guid.NewGuid()),
            CancellationToken.None);

        await act.Should()
            .ThrowAsync<DomainException>()
            .WithMessage("Empréstimo não encontrado.");
    }
}