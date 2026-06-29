using MassTransit;
using MongoDB.Driver;
using Scalar.AspNetCore;
using TechnicalTestOpea.Adapters.Infraestructure.Messaging;
using TechnicalTestOpea.Adapters.Infraestructure.Messaging.Services;
using TechnicalTestOpea.Adapters.Infraestructure.NoSQL;
using TechnicalTestOpea.Adapters.Infraestructure.RelationalDataBase;
using TechnicalTestOpea.Core.Application;
using TechnicalTestOpea.Core.Application.Abstractions.Messaging;
using TechnicalTestOpea.Ports.OperationAPI.Middlewares;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi("v1");

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddSqlServerInfrastructure(builder.Configuration);

builder.Services.AddScoped<IIntegrationEventPublisher, MessageBroker>();
builder.Services.AddNoSqlDataBase(builder.Configuration);

#region RabbitMQ
var msgSetings = builder.Configuration.GetSection("MessageBrokerSettings").Get<MessageBrokerSettings>()!;
builder.Services.AddSingleton(msgSetings);
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        var settings = context.GetRequiredService<MessageBrokerSettings>();

        cfg.Host(settings.Host, settings.VirtualHost, h =>
            {
                h.Username(settings.User);
                h.Password(settings.Pass);
            });
    });
});

#endregion

var app = builder.Build();

app.MapHealthChecks("/health");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi("/openapi/{documentName}.json");

    app.MapScalarApiReference(options =>
    {
        options.OpenApiRoutePattern = "/openapi/{documentName}.json";
        options.Title = "Technical Test Opea API";
    });
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
