using System.Text;

namespace Dishes.Application.Abstractions.Errors;

public static class DishesErrors
{
    public static Error NotFound(Guid dishId) =>
        new(ErrorType.NotFound, $"The dish with id '{dishId}' was not found.");

    public static Error NotFound(string dishName) =>
        new(ErrorType.NotFound, $"The dish with id '{dishName}' was not found.");

    public static Error Null => 
        new(ErrorType.Null, "Error creating the token.");

    public static Error NotSaved(string dishName) =>
        new(ErrorType.NotSaved, $"Error occurred saving dish with {dishName}.");

    public static Error NotSaved(Guid? dishId = null, string? dishName = null, string? message = null)
    {
        const string errorMessage = "Error occurred";

        var stringBuilder = new StringBuilder();

        stringBuilder.Append(errorMessage);

        if (dishId is not null)
        {
            stringBuilder.Append($" for dish with id `{dishId}`");
        }

        if (!string.IsNullOrWhiteSpace(dishName))
        {
            stringBuilder.Append($" for dish with name `{dishName}`");
        }

        if (!string.IsNullOrWhiteSpace(message))
        {
            stringBuilder.Clear();
            stringBuilder.Append(errorMessage);
            stringBuilder.Append(message);
        }

        return new(ErrorType.NotSaved, stringBuilder.ToString());
    }
}