using Dishes.Domain.Entities;
using Dishes.Infrastructure.DbContexts;
using Dishes.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Dishes.Infrastructure.Repositories;

internal class DishesRepository(ILogger<DishesRepository> logger, DishesDbContext dishesDbContext) : IDishesRepository
{
    public async Task<Dish?> GetDishByIdAsync(Guid dishId, bool shouldIncludeIngredients, CancellationToken cancellationToken = default)
    {
        try
        {
            if (shouldIncludeIngredients)
            {
                return await dishesDbContext
                    .Dishes
                    .Include(i => i.Ingredients)
                    .FirstOrDefaultAsync(x => x.Id == dishId, cancellationToken);
            }

            return await dishesDbContext
                .Dishes
                .FirstOrDefaultAsync(x => x.Id == dishId, cancellationToken);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error occurred when retrieving dish with {Id}.", dishId);
            throw;
        }
    }

    public async Task<Dish?> GetDishByNameAsync(string dishName, bool shouldIncludeIngredients, CancellationToken cancellationToken = default)
    {
        try
        {
            if (shouldIncludeIngredients)
            {
                return await dishesDbContext
                    .Dishes
                    .Include(i => i.Ingredients)
                    .FirstOrDefaultAsync(x => x.Name == dishName, cancellationToken);
            }

            return await dishesDbContext
                .Dishes
                .FirstOrDefaultAsync(x => x.Name == dishName, cancellationToken);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error occurred when retrieving dish with {Name}.", dishName);
            throw;
        }
    }

    public async Task<IEnumerable<Dish>> GetDishesAsync(string? name, bool shouldIncludeIngredients, CancellationToken cancellationToken = default)
    {
        try
        {
            IQueryable<Dish> collection = dishesDbContext.Dishes;

            if (!string.IsNullOrWhiteSpace(name))
            {
                collection = collection.Where(x => x.Name.Contains(name));
            }

            if (shouldIncludeIngredients)
            {
                return await collection
                    .Include(i => i.Ingredients)
                    .ToListAsync(cancellationToken);
            }

            return await collection.ToListAsync(cancellationToken);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error occurred when retrieving dishes.");
            throw;
        }
    }

    public async Task<IEnumerable<Ingredient>> GetIngredientsByDishIdAsync(Guid dishId, CancellationToken cancellationToken = default)
    {
        try
        {
            var dish = await dishesDbContext
                .Dishes
                .Include(i => i.Ingredients)
                .FirstOrDefaultAsync(x => x.Id == dishId, cancellationToken);

            return dish?.Ingredients?.Count > 0
                ? dish.Ingredients
                : [];
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error occurred when retrieving dishes.");
            throw;
        }
    }
}