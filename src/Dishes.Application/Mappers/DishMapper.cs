using Dishes.Application.Dtos;
using Dishes.Application.Interfaces;
using Dishes.Domain.Entities;

namespace Dishes.Application.Mappers;

public class DishMapper : IMapper
{
    public DishesDto Map(Dish dish)
    {
        var ingredientsDtos = dish
            .Ingredients
            .Select(x => new IngredientDto(x.Id, x.Name, dish.Id))
            .ToList();

        return new DishesDto(dish.Id, dish.Name, ingredientsDtos);
    }

    public IEnumerable<DishesDto> Map(IEnumerable<Dish> dishes)
    {
        return dishes.Select(Map);
    }

    public IEnumerable<IngredientDto> Map(Guid dishId, IEnumerable<Ingredient> ingredients)
    {
        return ingredients
            .Select(x => new IngredientDto(x.Id, x.Name, dishId))
            .ToList();
    }
}