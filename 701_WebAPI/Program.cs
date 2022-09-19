using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using _701_WebAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using _701_WebAPI.Models.JWT;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<_701_WebAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("_701_WebAPIContext")));

builder.Services.AddControllers();
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

string domain = "https://dev-a3wdzleo.us.auth0.com/";
string identifier = "https://701-WebAPI-Auth0/api"; //change in appsettings.json

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = domain;
    options.Audience = identifier;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        NameClaimType = ClaimTypes.NameIdentifier
    };
}); //Jwt

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("read:everything", policy => policy.Requirements.Add(new HasScopeRequirement("read:everything", domain)));
}); //Jwt

builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>(); //Jwt

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(); //Cors

app.UseAuthentication(); //Jwt

app.UseAuthorization();

app.MapControllers();

app.Run();
