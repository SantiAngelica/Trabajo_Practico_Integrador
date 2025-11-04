using System.Text.RegularExpressions;
using Application.Models;

namespace Application.Helpers;

public static class UserHelper
{
    public static string? IsValidUserData(
        string Email,
        int Age,
        List<string> Positions,
        List<int> FieldsType
    )
    {
        if (!Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            return "Invalid email format.";
        }
        if (Age < 12 || Age > 110)
        {
            return "Age must be between 12 and 110.";
        }
        List<string> validPositions = new List<string>
        {
            "arquero",
            "defensor",
            "mediocampista",
            "delantero",
        };
        if (!Positions.All(p => validPositions.Contains(p.ToLower())))
        {
            return "One or more positions are invalid.";
        }
        var validFieldTypes = new List<int> { 5, 6, 7, 9, 8, 11 };
        if (!FieldsType.All(ft => validFieldTypes.Contains(ft)))
        {
            return "One or more field types are invalid.";
        }
        return null;
    }
}
