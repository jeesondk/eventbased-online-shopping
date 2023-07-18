using Common.Domain.User.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using UserService;
using UserService.Context;
using UserService.Infrastructure;
using UserService.Users;
using UserService.Users.Commands;
using UserService.Users.Queries;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUserCommands, UserCommands>();
        services.AddTransient<IUserQueries, UserQueries>();
        services.AddTransient<IUserService, Service>();
        services.AddTransient<IEventPublisher<GetUserResponse>, EventPublisher<GetUserResponse>>();
        services.AddTransient<IEventPublisher<CreateUserResponse>, EventPublisher<CreateUserResponse>>();
        
        services.AddMassTransit(opt =>
        {
            opt.AddConsumer<EventConsumer<CreateUser>>();
            opt.AddConsumer<EventConsumer<GetUser>>();

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