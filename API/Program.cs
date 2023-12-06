using Application;
using Infrastructure.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Konfigurera tjänster
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
});

// Registrera IMockDatabase
builder.Services.AddSingleton<IMockDatabase, MockDatabase>();

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
app.UseAuthorization();
app.MapControllers();
app.Run();
