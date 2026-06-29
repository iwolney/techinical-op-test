using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechnicalTestOpea.Core.Application.Features.Books.Commands.CreateBook;
using TechnicalTestOpea.Core.Application.Queries.Books.GetBookById;
using TechnicalTestOpea.Core.Application.Queries.Books.GetBooks;
using TechnicalTestOpea.Ports.OperationAPI.Models;

namespace TechnicalTestOpea.Ports.OperationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class BookController(ISender sender) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateBookCommand(
                request.Title,
                request.Author,
                request.PublicationYear,
                request.QuantityAvailable);

            var id = await sender.Send(command, cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id },
                new { id });
        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var book = await sender.Send(new GetBookByIdQuery(id), cancellationToken);

            return book is null
                ? NotFound()
                : Ok(book);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var books = await sender.Send(new GetBooksQuery(), cancellationToken);

            return Ok(books);
        }
    }
}
