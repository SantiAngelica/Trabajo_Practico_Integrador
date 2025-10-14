using System.Text.RegularExpressions;
using Application.Models;

namespace Application.Helpers;

public static class UserHelper
{
    public static string? IsValidUserData(RequestUserDto requestUser)
    {
        if (!Regex.IsMatch(requestUser.Password, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            return "Invalid email format.";
        }
        if (requestUser.Age < 12 || requestUser.Age > 110)
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
        if (!requestUser.Positions.All(p => validPositions.Contains(p.ToLower())))
        {
            return "One or more positions are invalid.";
        }
        var validFieldTypes = new List<int> { 5, 6, 7, 9, 8, 11 };
        if (!requestUser.FieldsType.All(ft => validFieldTypes.Contains(ft)))
        {
            return "One or more field types are invalid.";
        }
        return null;
    }
}
