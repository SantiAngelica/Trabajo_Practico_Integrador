using Core.Exceptions;

namespace Application.Helpers;

public static class GameHelper
{
    public static bool IsInAWeek(DateOnly date)
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
        DateOnly weekFromToday = today.AddDays(7);
        return date >= today && date <= weekFromToday;
    }

    public static void ValidateFieldAndSchedulue(int field, int sch)
    {
        var validFieldTypes = new List<int> { 5, 6, 7, 9, 8, 11 };
        if (!validFieldTypes.Contains(field))
            throw new AppValidationException("Field incorrect");

        if (sch < 0 || sch > 24)
            throw new AppValidationException("Schedule incorrect");
    }
}
