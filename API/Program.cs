using Application;
using Infrastructure.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer; // Lägg till detta using
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;
using System.Text; // Lägg till detta using
using Infrastructure;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Konfigurera tjänster
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
});

// Lägg till JWT-bearer-autentisering
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Konfigurera dina JWT-alternativ här, t.ex. issuer, audience, validering av lösenord, etc.
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            // Exempelinställningar, du måste anpassa detta baserat på din JWT-konfiguration
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Här-kommer-din-hemliga-nyckel")) // Byt ut detta mot din verkliga säkerhetsnyckel
        };
    });

// Lägg till din databas (ApiMainContext) som en tjänst med SQLite
builder.Services.AddDbContext<ApiMainContext>(options =>
{
    options.UseSqlite("Data Source=mydatabase.db");
});
// Registrera MediatR
builder.Services.AddApplication().AddInfrastructure();

// Skapa DI-kontainern
var app = builder.Build();

// Konfigurera HTTP-request-pipelinen
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1"));
}

app.UseHttpsRedirection();
app.UseAuthentication(); // Lägg till detta för att aktivera autentisering
app.UseAuthorization();
app.MapControllers();
app.Run();
