using Dishes.Application.Dtos;
using Dishes.Application.Interfaces;
using Dishes.Domain.Entities;

namespace Dishes.Application.Mappers;

public class DishMapper : IMapper
{
    public DishesDto Map(Dish dish)
    {
        var ingredientDtos = dish
            .Ingredients
            .Select(CreateIngredientDto)
            .ToList();

        return new DishesDto(dish.Id, dish.Name, ingredientDtos);
    }

    private static IngredientDto CreateIngredientDto(Ingredient ingredient)
    {
        return new IngredientDto(ingredient.Id, ingredient.Name);
    }
}