using Dishes.Application.Dtos;
using Dishes.Domain.Entities;

namespace Dishes.Application.Interfaces;

public interface IMapper
{
    DishesDto Map(Dish dish);
    IEnumerable<DishesDto> Map(IEnumerable<Dish> dishes);
    IEnumerable<IngredientDto> Map(Guid dishId, IEnumerable<Ingredient> ingredients);
}