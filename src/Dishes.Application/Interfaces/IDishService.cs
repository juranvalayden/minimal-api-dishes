using Dishes.Application.Dtos;

namespace Dishes.Application.Interfaces;

public interface IDishService
{
    Task<DishesDto?> GetDishByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
