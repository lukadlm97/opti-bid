using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OptiBid.Microservices.Accounts.Domain.Configuration;
using OptiBid.Microservices.Accounts.Grpc.Services;
using OptiBid.Microservices.Accounts.Messaging.Send;
using OptiBid.Microservices.Accounts.Messaging.Send.Configurations;
using OptiBid.Microservices.Accounts.Persistence;
using OptiBid.Microservices.Accounts.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc().AddJsonTranscoding(); 
builder.Services.AddGrpcSwagger();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo { Title = "gRPC transcoding", Version = "v1" });
});
builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings"));
builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMq"));
builder.Services.AddDbContext<AccountsContext>(options =>
    options
        .UseSqlServer(builder.Configuration.GetSection("DbSettings")["ConnectionString"]));


builder.Services.AddApplication();
builder.Services.AddMessageProducing();

var app = builder.Build(); 

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<DashboardService>();
app.MapGrpcService<UserService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
