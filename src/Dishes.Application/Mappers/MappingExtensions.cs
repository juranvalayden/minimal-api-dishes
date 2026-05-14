using Dishes.Application.Dtos;
using Dishes.Domain.Entities;

namespace Dishes.Application.Mappers;

public static class MappingExtensions
{
    public static DishDto ToDishDto(this Dish dish)
    {
        return new DishDto(dish.Id, dish.Name);
    }

    public static IEnumerable<DishDto> ToDishDtos(this IEnumerable<Dish> dishes)
    {
        return dishes.Select(d => d.ToDishDto());
    }

    public static IngredientDto ToIngredientDto(this Ingredient ingredient, Guid dishId)
    {
        return new IngredientDto(ingredient.Id, ingredient.Name, dishId);
    }

    public static IEnumerable<IngredientDto> ToIngredientDtos(this IEnumerable<Ingredient> ingredients, Guid dishId)
    {
        return ingredients.Select(i => i.ToIngredientDto(dishId));
    }

    public static Dish ToDish(this DishDto dishDto)
    {
        return new Dish
        {
            Id = dishDto.Id,
            Name = dishDto.Name
        };
    }

    public static IEnumerable<Dish> ToDish(this IEnumerable<DishDto> dishDtos)
    {
        return dishDtos.Select(d => d.ToDish());
    }

    public static Dish ToDish(this DishForCreationDto dishForCreationDto)
    {
        return new Dish
        {
            Name = dishForCreationDto.Name
        };
    }

    public static Dish ToDishForUpdate(this DishForUpdateDto dishForUpdateDto)
    {
        return new Dish
        {
            Name = dishForUpdateDto.Name
        };
    }

    /// <summary>
    /// Updates the existing dish with the name values
    /// </summary>
    /// <param name="dish">The current dish entity</param>
    /// <param name="dishForUpdateDto">The dish with the updates that will be applied to the current dish</param>
    /// <returns>The current entity that has been updated</returns>
    public static Dish ToDishForUpdate(this Dish dish, DishForUpdateDto dishForUpdateDto)
    {
        dish.Name = dishForUpdateDto.Name;
        return dish;
    }
}