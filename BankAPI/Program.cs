using Microsoft.EntityFrameworkCore;
using Services;
using BankContext = DbModels.BankContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BankContext>(x =>
    x.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=postgres;Port=15432"));
builder.Services.AddScoped<BankContext>();
builder.Services.AddScoped<ClientServiceDB>();
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
