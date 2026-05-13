using Dishes.Application.Abstractions.Errors;
using Dishes.Application.Dtos;

namespace Dishes.Application.Interfaces;

public interface IDishService
{
    Task<Response> GetDishByIdAsync(Guid id, bool shouldIncludeIngredients, CancellationToken cancellationToken = default);
    Task<Response> GetDishByNameAsync(string dishName, bool shouldIncludeIngredients, CancellationToken cancellationToken = default);
    Task<Response> GetDishesAsync(string? name, bool shouldIncludeIngredients, CancellationToken cancellationToken = default);
    Task<Response> GetIngredientsByDishIdAsync(Guid dishId, CancellationToken cancellationToken = default);

    Task<Response> AddAsync(DishForCreationDto dishForCreationDto, CancellationToken cancellationToken = default);
    Task<Response> UpdateAsync(Guid dishId, DishForUpdateDto dishForUpdateDto,
        CancellationToken cancellationToken = default);
    Task<Response> DeleteAsync(Guid dishId, CancellationToken cancellationToken = default);
}
