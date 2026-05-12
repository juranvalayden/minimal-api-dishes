using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Dishes.Application.Dtos;

public record DishesDto(Guid Id, string Name, ICollection<IngredientDto> Ingredients);
