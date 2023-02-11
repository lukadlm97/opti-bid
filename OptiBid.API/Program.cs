using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Connections;
using OptiBid.API.Consumers;
using OptiBid.API.Hubs;
using OptiBid.API.Producer;
using OptiBid.Microservices.Messaging.Receving.Configuration;
using OptiBid.Microservices.Messaging.Receving.Factories;
using OptiBid.Microservices.Messaging.Receving.MessageQueue;
using OptiBid.Microservices.Messaging.Receving.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMq"));
builder.Services.Configure<ChannelSettings>(builder.Configuration.GetSection("ChannelSettings"));
builder.Services.Configure<RabbitMqQueueSettings>(builder.Configuration.GetSection("RabbitQueueName"));

builder.Services.AddControllers();
builder.Services.AddSingleton<IMessageQueue, NotificationMessageQueue>();
builder.Services.AddSingleton<IMqConnectionFactory, RabbitMqConnectionFactory>();
builder.Services.AddSingleton<NotificationHub>();
builder.Services.AddSingleton<ConnectionManager>();

//builder.Services.AddHostedService<NotificationService>();
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
