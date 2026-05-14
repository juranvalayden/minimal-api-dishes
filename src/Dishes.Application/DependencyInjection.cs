using Dishes.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
using IDishService = Dishes.Application.Interfaces.IDishService;

namespace Dishes.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddProblemDetails();
        services.AddValidation();

        services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, context, cancellationToken) =>
            {
                document.Info = new OpenApiInfo
                {
                    Title = "Dishes.WebApi",
                    Version = "v1",
                    Description = "An API for managing dishes and their ingredients."
                };

                document.Components ??= new OpenApiComponents();
                document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();
                document.Components.SecuritySchemes["Bearer"] = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "Enter a valid JWT bearer token."
                };

                document.Security ??= [];
                document.Security.Add(new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference("Bearer")] = [],
                });

                return Task.CompletedTask;
            });
        });

        services.AddScoped<IDishService, DishService>();
    }
}