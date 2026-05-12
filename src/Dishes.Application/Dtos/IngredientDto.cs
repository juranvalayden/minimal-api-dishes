namespace Dishes.Application.Dtos;

public record IngredientDto(Guid Id, string Name, Guid DishId);