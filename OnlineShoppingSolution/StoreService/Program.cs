using Common.Domain.Shop.Events;
using Common.Domain.User.Events;
using Marten;
using MassTransit;
using StoreService;
using StoreService.Infrastructure;

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
            opt.AddConsumer<EventConsumer<NewSession>>();
            opt.AddConsumer<EventConsumer<AddItem>>();
            opt.AddConsumer<EventConsumer<RemoveItem>>();
            opt.AddConsumer<EventConsumer<GetSession>>();
            opt.AddConsumer<EventConsumer<CheckOut>>();
             
            opt.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(hostContext.Configuration.GetValue<string>("RabbitMQ:Host"), "/", host =>
                {
                    host.Username(hostContext.Configuration.GetValue<string>("RabbitMQ:Username"));
                    host.Password(hostContext.Configuration.GetValue<string>("RabbitMQ:secret"));
                });

                cfg.ConfigureEndpoints(ctx);
            });
        });
        
        services.AddTransient<IService, Service>();
    })
    .Build();

await host.RunAsync();