using Dishes.Domain.Entities;

namespace Dishes.Infrastructure.Interfaces;

public interface IDishesRepository
{
    Task<IEnumerable<Dish>> GetDishesAsync(string? name, bool shouldIncludeIngredients, CancellationToken cancellationToken = default);
    Task<Dish?> GetDishByIdAsync(Guid dishId, bool shouldIncludeIngredients, CancellationToken cancellationToken = default);
    Task<Dish?> GetDishByNameAsync(string dishName, bool shouldIncludeIngredients, CancellationToken cancellationToken = default);

    Task<IEnumerable<Ingredient>> GetIngredientsByDishIdAsync(Guid dishId, CancellationToken cancellationToken = default);
}