using MassTransit;
using Microsoft.EntityFrameworkCore;
using UserService.Context;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddMassTransit(opt =>
        {
            //opt.AddConsumer<EventConsumer<CreateUser>>();

            opt.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(hostContext.Configuration.GetValue<string>("RabbitMQ:Host"), "/", host =>
                {
                    host.Username(hostContext.Configuration.GetValue<string>("RabbitMQ:Username"));
                    host.Password(hostContext.Configuration.GetValue<string>("RabbitMQ:Password"));
                });
    
                cfg.ConfigureEndpoints(ctx);
            });
        });
        services.AddDbContext<UserContext>(opt =>
                opt.UseNpgsql(hostContext.Configuration.GetConnectionString("UsersDb")!),
            contextLifetime: ServiceLifetime.Transient,
            optionsLifetime: ServiceLifetime.Transient);
        
    })
    .Build();

using (var scope = host.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<UserContext>();
    
    // Here is the migration executed
    dbContext.Database.Migrate();
}

await host.RunAsync();