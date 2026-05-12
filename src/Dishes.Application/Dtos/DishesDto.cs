namespace Dishes.Application.Dtos;

public record DishesDto(Guid Id, string Name, ICollection<IngredientDto> Ingredients);
