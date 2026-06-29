using MediatR;
using TechnicalTestOpea.Core.Application.Abstractions.Persistence;
using TechnicalTestOpea.Core.Domain.Common;
using TechnicalTestOpea.Core.Domain.Entities;

namespace TechnicalTestOpea.Core.Application.Features.Loans.Commands.CreateLoan
{
    public sealed class CreateLoanCommandHandler(
        IBookRepository bookRepository,
        ILoanRepository loanRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<CreateLoanCommand, Guid>
    {
        public async Task<Guid> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
        {
            var book = await bookRepository.GetByIdAsync(request.BookId, cancellationToken);

            if(book is null)
                throw new DomainException("Livro não encontrado.");

            var loan = Loan.Create(book, request.BorrowerName);

            bookRepository.Update(book);

            await loanRepository.AddAsync(loan, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return loan.Id;
        }
    }
}
