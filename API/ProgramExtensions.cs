using Application.Commands.Cats;
using Application.Commands.Cats.AddCat;
using Domain.Models;
using Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceExtensions
{
    public static IServiceCollection AddCatServices(this IServiceCollection services)
    {
        services.AddTransient<IRequestHandler<AddCatCommand, Cat>, AddCatCommandHandler>();
        // Register additional cat-related services...

        return services;
    }
}

