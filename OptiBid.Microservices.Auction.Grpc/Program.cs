using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OptiBid.Microservices.Auction.Domain.Configurations;
using OptiBid.Microservices.Auction.Grpc.Services;
using OptiBid.Microservices.Auction.Messaging.Sender;
using OptiBid.Microservices.Auction.Messaging.Sender.Configurations;
using OptiBid.Microservices.Auction.Persistence;
using OptiBid.Microservices.Auction.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
builder.Services.AddGrpc();
builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings"));
builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMq"));
builder.Services.Configure<MqSettings>(builder.Configuration.GetSection("MqSettings"));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<RabbitMqSettings>>().Value);
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<MqSettings>>().Value);
builder.Services.AddDbContext<AuctionContext>(options =>
    options.UseNpgsql(builder.Configuration.GetSection("DbSettings")["ConnectionString"]));
builder.Services.AddApplication();
builder.Services.AddMessageProducing();
// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<CategoryService>();
app.MapGrpcService<CustomerService>();
app.MapGrpcService<AuctionAssetsService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
