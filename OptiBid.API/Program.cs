using Microsoft.AspNetCore.Http.Connections;
using Microsoft.Extensions.DependencyInjection;
using OptiBid.API.Hubs;
using OptiBid.API.Producer;
using OptiBid.Microservices.Contracts.Configuration;
using OptiBid.Microservices.Messaging.Receving.Configuration;
using OptiBid.Microservices.Messaging.Receving.Consumers;
using OptiBid.Microservices.Messaging.Receving.Factories;
using OptiBid.Microservices.Messaging.Receving.MessageQueue;
using OptiBid.Microservices.Messaging.Receving.Models;
using OptiBid.Microservices.Services.Utilities;
using OptiBid.Microservices.Shared.Caching.Configuration;
using OptiBid.Microservices.Shared.Caching.Utilities;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMq"));
builder.Services.Configure<ChannelSettings>(builder.Configuration.GetSection("ChannelSettings"));
builder.Services.Configure<RabbitMqQueueSettings>(builder.Configuration.GetSection("RabbitQueueName"));
builder.Services.Configure<ExternalGrpcSettings>(builder.Configuration.GetSection("ExternalGrpcSettings"));
builder.Services.Configure<HybridCacheSettings>(builder.Configuration.GetSection(nameof(HybridCacheSettings)));

builder.Services.AddControllers();
builder.Services.AddSingleton<IAccountMessageQueue, AccountNotificationMessageQueue>();
builder.Services.AddSingleton<IAuctionMessageQueue, AuctionNotificationMessageQueue>();
builder.Services.AddSingleton<IBidMessageQueue, BidNotificationMessageQueue>();
builder.Services.AddSingleton<IMqConnectionFactory, RabbitMqConnectionFactory>();
builder.Services.AddSingleton<NotificationHub>();
builder.Services.AddSingleton<ConnectionManager>();
builder.Services.AddAccountApplication();
builder.Services.AddHybridCaching();

builder.Services.AddHostedService<AccountNotificationService>();
builder.Services.AddHostedService<AuctionNotificationService>();
builder.Services.AddHostedService<BidNotificationService>();
builder.Services.AddHostedService<AuctionConsumer>();
builder.Services.AddHostedService<AccountConsumer>();
builder.Services.AddSignalR()
    .AddHubOptions<NotificationHub>(options =>
    {
        options.EnableDetailedErrors = true;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<NotificationHub>("/notification", options =>
    {
        options.Transports =
            HttpTransportType.WebSockets |
            HttpTransportType.LongPolling;
    });
});
app.MapControllers();


app.Run();
