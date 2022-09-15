using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using _701_WebAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<_701_WebAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("_701_WebAPIContext")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
}); //Cors

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(); //Cors

app.UseAuthorization();

app.MapControllers();

app.Run();
