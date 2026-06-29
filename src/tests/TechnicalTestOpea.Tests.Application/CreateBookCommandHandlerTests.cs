using FluentAssertions;
using Moq;
using TechnicalTestOpea.Core.Application.Abstractions.Persistence;
using TechnicalTestOpea.Core.Application.Features.Books.Commands.CreateBook;
using TechnicalTestOpea.Core.Domain.Entities;

namespace TechnicalTestOpea.Tests.Application;

public class CreateBookCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldCreateBook()
    {
        var bookRepository = new Mock<IBookRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        unitOfWork
            .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var handler = new CreateBookCommandHandler(
            bookRepository.Object,
            unitOfWork.Object);

        var command = new CreateBookCommand(
            "Clean Code",
            "Robert C. Martin",
            2008,
            5);

        var id = await handler.Handle(command, CancellationToken.None);

        id.Should().NotBeEmpty();

        bookRepository.Verify(
            x => x.AddAsync(
                It.Is<Book>(b =>
                    b.Title == command.Title &&
                    b.Author == command.Author &&
                    b.PublicationYear == command.PublicationYear &&
                    b.QuantityAvailable == command.QuantityAvailable),
                It.IsAny<CancellationToken>()),
            Times.Once);

        unitOfWork.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once);
    }
}