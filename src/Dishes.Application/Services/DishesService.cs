using Dishes.Application.Dtos;
using Dishes.Application.Interfaces;
using Dishes.Infrastructure.Interfaces;

namespace Dishes.Application.Services;

public class DishesService(IDishesRepository dishRepository, IMapper mapper) : IDishService
{
    public async Task<IEnumerable<DishesDto>> GetDishesAsync(string? name, bool shouldIncludeIngredients, CancellationToken cancellationToken = default)
    {
        var entities = await dishRepository.GetDishesAsync(name, shouldIncludeIngredients, cancellationToken);

        var dishes = entities.ToList();

        return dishes.Count == 0 
            ? [] 
            : mapper.Map(dishes);
    }

    public async Task<DishesDto?> GetDishByIdAsync(Guid id, bool shouldIncludeIngredients, CancellationToken cancellationToken = default)
    {
        var entity = await dishRepository.GetDishByIdAsync(id, shouldIncludeIngredients, cancellationToken);

        return entity is null 
            ? null 
            : mapper.Map(entity);
    }
    
    public async Task<DishesDto?> GetDishByNameAsync(string dishName, bool shouldIncludeIngredients, CancellationToken cancellationToken = default)
    {
        var entity = await dishRepository.GetDishByNameAsync(dishName, shouldIncludeIngredients, cancellationToken);

        return entity is null
            ? null
            : mapper.Map(entity);
    }

    public async Task<IEnumerable<IngredientDto>> GetIngredientsByDishIdAsync(Guid dishId, CancellationToken cancellationToken = default)
    {
        var entities = await dishRepository.GetIngredientsByDishIdAsync(dishId, cancellationToken);

        var ingredients = entities.ToList();

        return ingredients.Count == 0
            ? []
            : mapper.Map(dishId, ingredients);
    }
}