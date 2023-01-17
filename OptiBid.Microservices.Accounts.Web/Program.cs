using Microsoft.EntityFrameworkCore;
using OptiBid.Microservices.Accounts.Domain.Configuration;
using OptiBid.Microservices.Accounts.Persistence;
using System.Reflection;
using MediatR;
using OptiBid.Microservices.Accounts.Data.Repository;
using Microsoft.OpenApi.Models;
using OptiBid.Microservices.Accounts.Domain.Entities;
using OptiBid.Microservices.Accounts.Services.Query.Accounts;
using OptiBid.Microservices.Accounts.Services.Query.ContactType;
using OptiBid.Microservices.Accounts.Services.Query.Country;
using OptiBid.Microservices.Accounts.Services.Query.Profession;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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


builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IRequestHandler<GetAccountsCommand, List<User>>, GetAccountsQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetCountriesCommand, List<Country>>, GetCountriesQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetContactTypeCommand, List<ContactType>>, GetContactTypeQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetProfessionsCommand, List<Profession>>, GetProfessionsQueryHandler>();

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
