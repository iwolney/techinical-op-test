using MassTransit;
using TechnicalTestOpea.Adapters.Infraestructure.Messaging;
using TechnicalTestOpea.Adapters.Infraestructure.NoSQL;
using TechnicalTestOpea.Ports.DataReplicationConsumer.Consumers;

var builder = Host.CreateApplicationBuilder(args);


builder.Services.AddNoSqlDataBase(builder.Configuration);


var msgSettings = builder.Configuration.GetSection("MessageBrokerSettings").Get<MessageBrokerSettings>() 
    ?? throw new InvalidOperationException("MessageBrokerSettings não foi carregado.");

builder.Services.AddSingleton(msgSettings);

builder.Services.AddMassTransit(x =>
{
    
    x.AddConsumer<BookCreatedIntegrationEventConsumer>();
    x.AddConsumer<LoanCreatedIntegrationEventConsumer>();
    x.AddConsumer<LoanReturnedIntegrationEventConsumer>();


    x.UsingRabbitMq((context, cfg) =>
    {
        var settings = context.GetRequiredService<MessageBrokerSettings>();

        cfg.Host(settings.Host, settings.VirtualHost, h =>
        {
            h.Username(settings.User);
            h.Password(settings.Pass);
        });

        cfg.ConfigureEndpoints(context);
    });
});


var host = builder.Build();
host.Run();
