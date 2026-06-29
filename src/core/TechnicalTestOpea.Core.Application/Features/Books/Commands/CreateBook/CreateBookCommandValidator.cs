using FluentValidation;

namespace TechnicalTestOpea.Core.Application.Features.Books.Commands.CreateBook
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Author)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.PublicationYear)
                .GreaterThan(0);

            RuleFor(x => x.QuantityAvailable)
                .GreaterThanOrEqualTo(0);
        }
    }
}
