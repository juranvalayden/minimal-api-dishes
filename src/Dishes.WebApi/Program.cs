using Dishes.Application;
using Dishes.Application.Extensions;
using Dishes.Infrastructure;
using Dishes.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.RegisterDishesEndpoints();
app.RegisterIngredientsEndpoints();

using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
var serviceProvider = serviceScope.ServiceProvider;
var dishesDbContext = serviceProvider.GetRequiredService<DishesDbContext>();
_ = await dishesDbContext.Database.EnsureDeletedAsync();
await dishesDbContext.Database.MigrateAsync();

app.Run();