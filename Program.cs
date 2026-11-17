using ConcertTicketBooking.Data;
using ConcertTicketBooking.Repositories.Implementations;
using ConcertTicketBooking.Repositories.Interfaces;
using ConcertTicketBooking.Services.Implementations;
using ConcertTicketBooking.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Register Controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Serialize enums as strings instead of numbers
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter());
    });

// Register OpenAPI
builder.Services.AddOpenApi();

// Configure SQL Server and EF Core
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Repository
builder.Services.AddScoped<IConcertRepository, ConcertRepository>();

// Register Service
builder.Services.AddScoped<IConcertService, ConcertService>();

var app = builder.Build();

// Configure HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();