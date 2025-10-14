namespace Application.Helpers;

public static class PropertyHelper
{
    public static string? IsValidPropertyData(List<int> fieldType, List<int> schedules)
    {
        var validFieldTypes = new List<int> { 5, 6, 7, 9, 8, 11 };
        if (!fieldType.All(ft => validFieldTypes.Contains(ft)))
        {
            return "One or more field types are invalid.";
        }
        if (schedules.All(s => s < 1 || s > 24))
        {
            return "Schedules must be between 1 and 24.";
        }
        return null;
    }
}