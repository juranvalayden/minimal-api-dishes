using Dishes.Application.Dtos;
using Dishes.Application.Interfaces;
using Dishes.Domain.Entities;

namespace Dishes.Application.Mappers;

public class DishMapper : IMapper
{
    public DishDto Map(Dish dish)
    {
        return new DishDto(dish.Id, dish.Name);
    }

    public IEnumerable<DishDto> Map(IEnumerable<Dish> dishes)
    {
        return dishes.Select(Map);
    }

    public IEnumerable<IngredientDto> Map(Guid dishId, IEnumerable<Ingredient> ingredients)
    {
        return ingredients
            .Select(x => new IngredientDto(x.Id, x.Name, dishId))
            .ToList();
    }

    public Dish MapDtoToEntity(DishForCreationDto dishForCreationDto)
    {
        var dishId = Guid.NewGuid();
        return new Dish(dishId, dishForCreationDto.Name);
    }

    public Dish MapDtoToEntity(Dish existingEntity, DishForUpdateDto dishForUpdateDto)
    {
        existingEntity.Name = dishForUpdateDto.Name;
        return existingEntity;
    }
}