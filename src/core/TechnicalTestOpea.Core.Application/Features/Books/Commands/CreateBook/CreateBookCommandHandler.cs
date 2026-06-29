using MediatR;
using TechnicalTestOpea.Core.Application.Abstractions.Persistence;
using TechnicalTestOpea.Core.Domain.Entities;

namespace TechnicalTestOpea.Core.Application.Features.Books.Commands.CreateBook
{
    public class CreateBookCommandHandler(
        IBookRepository bookRepository, 
        IUnitOfWork unitOfWork) 
        : IRequestHandler<CreateBookCommand, Guid>
    {
        public async Task<Guid> Handle( CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = Book.Create(
                request.Title,
                request.Author,
                request.PublicationYear,
                request.QuantityAvailable);

            await bookRepository.AddAsync(book, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return book.Id;
        }
    }
}
