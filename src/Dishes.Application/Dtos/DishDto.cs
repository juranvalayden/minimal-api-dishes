namespace Dishes.Application.Dtos;

public class DishDto
{
    public Guid Id { get; set; }

    public required string Name { get; set; }
}