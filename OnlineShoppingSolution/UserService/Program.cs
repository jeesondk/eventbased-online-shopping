using Common.Consumer;
using Marten;
using MassTransit;
using UserService.Domain.Events;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddMarten(opt =>
            {
                opt.Connection(hostContext.Configuration.GetConnectionString("EventStore")!);
            })
            .OptimizeArtifactWorkflow()
            .UseLightweightSessions()
            .InitializeWith();
        services.AddMassTransit(opt =>
        {
            opt.AddConsumer<EventConsumer<CreateUser>>();

            opt.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(hostContext.Configuration.GetValue<string>("RabbitMQ:Host"), "/", host =>
                {
                    host.Username(hostContext.Configuration.GetValue<string>("RabbitMQ:Username"));
                    host.Password(hostContext.Configuration.GetValue<string>("RabbitMQ:Password"));
                });
    
                cfg.ConfigureEndpoints(ctx);
            });    
            
        });})
    .Build();

await host.RunAsync();