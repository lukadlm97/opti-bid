using Microsoft.EntityFrameworkCore;
using OptiBid.Microservices.Auction.Domain.Configurations;
using OptiBid.Microservices.Auction.Grpc.Services;
using OptiBid.Microservices.Auction.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings"));
builder.Services.AddDbContext<AuctionContext>(options =>
    options.UseNpgsql(builder.Configuration.GetSection("DbSettings")["ConnectionString"]));
// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
