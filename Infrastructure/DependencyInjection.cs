using Infrastructure.Database;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Birds;
using Infrastructure.Repositories.Cats;
using Infrastructure.Repositories.Dogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // Registrera dina repositories här
            services.AddScoped<IBirdRepository, BirdRepository>();
            services.AddScoped<ICatRepository, CatRepository>();
            services.AddScoped<IDogRepository, DogRepository>();
            services.AddScoped<IUserAnimalRepository, UserAnimalRepository>();


            // Registrera din DbContext för SQLite
            services.AddDbContext<ApiMainContext>(options =>
            {
                options.UseSqlite("Data Source=api_main.db");
            });

            return services;
        }
    }
}
