namespace Danish.Holidays;

#if NET6_0_OR_GREATER
    public record Holiday(int Year, int Month, int Day, HolidayType Type);
#else
public class Holiday
{
    public Holiday(int year, int month, int day, HolidayType type)
    {
        Year = year;
        Month = month;
        Day = day;
        Type = type;
    }

    public int Year { get; }
    public int Month { get; }
    public int Day { get; }
    public HolidayType Type { get; }
}

#endif
