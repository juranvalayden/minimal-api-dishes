using Dishes.Domain.Entities;

namespace Dishes.Infrastructure.Interfaces;

public interface IDishesRepository
{
    Task<Dish?> GetDishByIdAsync(Guid id, CancellationToken cancellationToken = default);
}