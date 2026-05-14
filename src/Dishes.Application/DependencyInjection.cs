using Dishes.Application.Interfaces;
using Dishes.Application.Mappers;
using Dishes.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using IDishService = Dishes.Application.Interfaces.IDishService;

namespace Dishes.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddProblemDetails();

        services.AddScoped<IMapper, DishMapper>();
        services.AddScoped<IDishService, DishService>();
    }
}