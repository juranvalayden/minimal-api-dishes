using System.ComponentModel.DataAnnotations;

namespace Dishes.Application.Dtos;

public class DishForCreationDto
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public required string Name { get; set; }
}
