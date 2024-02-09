using Application;
using Infrastructure.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer; // L�gg till detta using
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;
using System.Text; // L�gg till detta using
using Infrastructure;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Konfigurera tj�nster
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
});

// L�gg till JWT-bearer-autentisering
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Konfigurera dina JWT-alternativ h�r, t.ex. issuer, audience, validering av l�senord, etc.
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            // Exempelinst�llningar, du m�ste anpassa detta baserat p� din JWT-konfiguration
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("H�r-kommer-din-hemliga-nyckel")) // Byt ut detta mot din verkliga s�kerhetsnyckel
        };
    });

// L�gg till din databas (ApiMainContext) som en tj�nst med SQLite
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
app.UseAuthentication(); // L�gg till detta f�r att aktivera autentisering
app.UseAuthorization();
app.MapControllers();
app.Run();
