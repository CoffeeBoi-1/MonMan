using MonMan.Models;
using MonMan.Data;
using Microsoft.EntityFrameworkCore;
using MonMan.Endpoints;

Console.WriteLine("Hi");

var builder = WebApplication.CreateBuilder(args);

// hook up PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.RegisterTransactionEndpoint();

app.Run();