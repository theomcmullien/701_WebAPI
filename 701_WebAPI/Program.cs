using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using _701_WebAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using _701_WebAPI.Models.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<_701_WebAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("_701_WebAPIContext")));

builder.Services.AddControllers();
builder.Services.AddControllers().AddControllersAsServices();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ILT Maintenance API", Version = "v1.0.0" });

    var securitySchema = new OpenApiSecurityScheme
    {
        Description = "Using the Authorization header with the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    c.AddSecurityDefinition("Bearer", securitySchema);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securitySchema, new[] { "Bearer" } }
    });
}); //Jwt Swagger?

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
}); //Cors

string domain = "https://dev-bss0r74x.au.auth0.com/";
string identifier = "https://dev-bss0r74x.au.auth0.com/api/v2/"; //change in appsettings.json

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = domain;
    options.Audience = identifier;
    //options.TokenValidationParameters = new TokenValidationParameters
    //{
    //    NameClaimType = ClaimTypes.NameIdentifier
    //};
}); //Jwt

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("read:everything", policy => policy.Requirements.Add(new HasScopeRequirement("read:everything", domain)));
//}); //Jwt

builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>(); //Jwt

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseCors(); //Cors

app.UseAuthentication(); //Jwt

app.UseAuthorization();

app.MapControllers();

app.Run();
