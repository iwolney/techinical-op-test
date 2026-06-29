using MediatR;
using TechnicalTestOpea.Core.Application.Abstractions.Persistence;
using TechnicalTestOpea.Core.Domain.Common;

namespace TechnicalTestOpea.Core.Application.Features.Loans.Commands.ReturnLoan
{
    public sealed class ReturnLoanCommandHandler(
        IBookRepository bookRepository,
        ILoanRepository loanRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<ReturnLoanCommand>
    {
        public async Task Handle(ReturnLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = await loanRepository.GetByIdAsync(request.LoanId, cancellationToken);

            if (loan is null)
                throw new DomainException("Empréstimo não encontrado.");

            var book = await bookRepository.GetByIdAsync(
                loan.BookId,
                cancellationToken);

            if (book is null)
                throw new DomainException("Book not found.");

            loan.Return(book);

            loanRepository.Update(loan);

            bookRepository.Update(book);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
