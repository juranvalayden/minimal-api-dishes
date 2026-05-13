using Dishes.Application.Dtos;
using Dishes.Domain.Entities;

namespace Dishes.Application.Interfaces;

public interface IMapper
{
    DishDto Map(Dish dish);
    IEnumerable<DishDto> Map(IEnumerable<Dish> dishes);
    IEnumerable<IngredientDto> Map(Guid dishId, IEnumerable<Ingredient> ingredients);
    Dish MapDtoToEntity(DishForCreationDto dishForCreationDto);
    Dish MapDtoToEntity(Dish existingEntity, DishForUpdateDto dishForUpdateDto);
}