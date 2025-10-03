namespace Application.Helpers;

public static class GameHelper
{
    public static bool IsInAWeek(DateOnly date)
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
        DateOnly weekFromToday = today.AddDays(7);
        return date >= today && date <= weekFromToday;
    }
}
