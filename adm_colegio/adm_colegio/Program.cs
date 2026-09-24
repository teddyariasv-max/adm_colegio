using Microsoft.EntityFrameworkCore;
using adm_colegio.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<db_context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("default_connection")));

// 1. Definir la política CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("allow_blazor", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 2. ACTIVAR CORS AQUÍ (debe ir ANTES de UseAuthorization)
app.UseCors("allow_blazor");

app.UseAuthorization();
app.MapControllers();

app.Run();