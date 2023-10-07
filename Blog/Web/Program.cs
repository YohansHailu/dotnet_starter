using Blog.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "Infrastructure/Data", "Database.db");

Console.WriteLine($"dbPath: {dbPath}");

builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
