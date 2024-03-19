using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using bruh.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<bruhContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("bruhContext") ?? throw new InvalidOperationException("Connection string 'bruhContext' not found.")));
builder.Services.AddScoped<AnimalService>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Allow all", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Allow all");

app.UseAuthorization();

app.MapControllers();

app.Run();
