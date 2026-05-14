using Dishes.Application;
using Dishes.Application.Extensions;
using Dishes.Infrastructure;
using Dishes.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsProduction())
{
    app.UseExceptionHandler();
}

if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    // may only want to expose this in dev/test environments
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseStatusCodePages();

// openapi/v1.json
app.MapOpenApi();

app.RegisterDishesEndpoints();
app.RegisterIngredientsEndpoints();

using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
var serviceProvider = serviceScope.ServiceProvider;
var dishesDbContext = serviceProvider.GetRequiredService<DishDbContext>();
_ = await dishesDbContext.Database.EnsureDeletedAsync();
await dishesDbContext.Database.MigrateAsync();

app.Run();