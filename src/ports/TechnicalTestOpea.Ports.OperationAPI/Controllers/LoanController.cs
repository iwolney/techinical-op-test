using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechnicalTestOpea.Core.Application.Features.Loans.Commands.CreateLoan;
using TechnicalTestOpea.Core.Application.Features.Loans.Commands.ReturnLoan;
using TechnicalTestOpea.Core.Application.Queries.Loans.GetLoans;
using TechnicalTestOpea.Ports.OperationAPI.Models;

namespace TechnicalTestOpea.Ports.OperationAPI.Controllers;

[ApiController]
[Route("api/loans")]
public sealed class LoansController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLoanRequest request, CancellationToken cancellationToken)
    {
        var id = await sender.Send(
            new CreateLoanCommand(
                request.BookId,
                request.BorrowerName),
            cancellationToken);

        return Created(string.Empty, new { id });        
    }

    [HttpPut("{id:guid}/return")]
    public async Task<IActionResult> Return(Guid id, CancellationToken cancellationToken)
    {
        await sender.Send(
            new ReturnLoanCommand(id),
            cancellationToken);

        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var loans = await sender.Send(
            new GetLoansQuery(),
            cancellationToken);

        return Ok(loans);
    }
}