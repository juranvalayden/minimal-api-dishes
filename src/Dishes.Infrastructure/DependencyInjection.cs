using Dishes.Infrastructure.DbContexts;
using Dishes.Infrastructure.Interfaces;
using Dishes.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dishes.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<DishesDbContext>(options =>
        {
            options.UseSqlite(connectionString);
        });

        services.AddScoped<IDishesRepository, DishesRepository>();
    }
}
