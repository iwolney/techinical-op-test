using System.Net;
using System.Text.Json;
using TechnicalTestOpea.Core.Domain.Common;

namespace TechnicalTestOpea.Ports.OperationAPI.Middlewares;

public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DomainException ex)
        {
            await WriteErrorAsync(
                context,
                HttpStatusCode.BadRequest,
                ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled error");

            await WriteErrorAsync(
                context,
                HttpStatusCode.InternalServerError,
                "Internal server error");
        }
    }

    private static async Task WriteErrorAsync(HttpContext context, HttpStatusCode statusCode, string message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            status = (int)statusCode,
            error = message
        };

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response));
    }
}