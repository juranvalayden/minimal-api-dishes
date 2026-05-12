using Dishes.Application.Dtos;

namespace Dishes.Application.Interfaces;

public interface IDishService
{
    Task<IEnumerable<DishesDto>> GetDishesAsync(string? name, bool shouldIncludeIngredients, CancellationToken cancellationToken = default);
    Task<DishesDto?> GetDishByIdAsync(Guid id, bool shouldIncludeIngredients, CancellationToken cancellationToken = default);
    Task<DishesDto?> GetDishByNameAsync(string dishName, bool shouldIncludeIngredients, CancellationToken cancellationToken = default);

    Task<IEnumerable<IngredientDto>> GetIngredientsByDishIdAsync(Guid dishId, CancellationToken cancellationToken = default);
}
