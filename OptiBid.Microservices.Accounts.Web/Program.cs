using Microsoft.EntityFrameworkCore;
using OptiBid.Microservices.Accounts.Domain.Configuration;
using OptiBid.Microservices.Accounts.Persistence;
using System.Reflection;
using MediatR;
using OptiBid.Microservices.Accounts.Data.Repository;
using Microsoft.OpenApi.Models;
using OptiBid.Microservices.Accounts.Services.Query.Accounts;
using OptiBid.Microservices.Accounts.Services.Query.ContactType;
using OptiBid.Microservices.Accounts.Services.Query.Country;
using OptiBid.Microservices.Accounts.Services.Query.Profession;
using Microsoft.Extensions.DependencyInjection;
using OptiBid.Microservices.Accounts.Services;
using System.Text.Json.Serialization;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
JsonSerializerOptions configOptions = new()
{
    ReferenceHandler = ReferenceHandler.Preserve,
    WriteIndented = true
};
builder.Services.AddControllers()
    .AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Accounts Api",
        Description = "A simple API to handle accounts",
        Contact = new OpenApiContact
        {
            Name = "Luka Radovanovic",
            Email = "lukadlm97@gmail.com",
           // Url = new Uri("")
        }
    });
});
builder.Services.AddSwaggerGen();
builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings"));
builder.Services.AddDbContext<AccountsContext>(options =>
        options
            .UseSqlServer(builder.Configuration.GetSection("DbSettings")["ConnectionString"]));


builder.Services.AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
