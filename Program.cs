
using ConcertTicketBooking.Data;
using ConcertTicketBooking.Repositories.Implementations;
using ConcertTicketBooking.Repositories.Interfaces;
using ConcertTicketBooking.Services.Implementations;
using ConcertTicketBooking.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// 1. Register Controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Serialize enums as strings instead of numbers
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter());
    });

// 2. Register OpenAPI
builder.Services.AddOpenApi();

// 3. Configure SQL Server and EF Core
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// 4. Register Repository
builder.Services.AddScoped<IConcertRepository, ConcertRepository>();

// 5. Register Service
builder.Services.AddScoped<IConcertService, ConcertService>();

// 6. Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Build application
var app = builder.Build();

// 7. Configure HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Enable CORS before authorization
app.UseCors("AllowAngular");

app.UseAuthorization();

app.MapControllers();

app.Run();
