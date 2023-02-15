using Microsoft.AspNetCore.Http.Connections;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
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
using System.Reflection;

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
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Auction API Gateway",
        Description = "An ASP.NET Core Web API for managing auction micro-services",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

});

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
