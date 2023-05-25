using System.Collections;
using System.Collections.Concurrent;

namespace Danish.Holidays;

internal class EasterUtil
{
    /// <summary>
    /// Returns the DateTime for easter-sunday for the specified year using the Lilius-Clavius algorithm.
    /// 
    /// Algorithm: http://www.henk-reints.nl/easter/.
    /// </summary>
    public static DateTime FindEasterSunday(int year)
    {
        var a = year % 19 + 1;
        var b = year / 100 + 1;
        var c = (3 * b) / 4 - 12;
        var d = (8 * b + 5) / 25 - 5;
        var e = (year * 5) / 4 - 10 - c;
        var f = ((11 * a + 20 + d - c) % 30 + 30) % 30;
        f = (f == 24 || (f == 25 && a > 11)) ? f + 1 : f;
        var g = 44 - f;
        g = (g < 21) ? g + 30 : g;

        // result: the date of March being Easter Sunday, even if this date > 31 (if so, it wraps into April)
        var result = g + 7 - (e + g) % 7;

        var easterSunday = new DateTime(year,3,1);
        easterSunday = easterSunday.AddDays(result - 1);
        return easterSunday;
        

    }
}



public class HolidayCollection 
{
    /// <summary>
    /// Returns all holidays. Even holidays that are no longer used
    /// </summary>
    public Holiday[] AllHolidays { get; }

    public int Year { get; }

    internal HolidayCollection(IEnumerable<Holiday> holidays, int year)
    {
        Year = year;
        AllHolidays = holidays.ToArray();
    }

    public IEnumerable<Holiday> GetHolidays()
    {
        if (Year <= 2023)
        {
            return AllHolidays;
        }
        else
        {
            return AllHolidays.Where(x => x.Type != HolidayType.StoreBededag);
        }
    }

    /// <summary>
    /// Gets the holiday for the given type. Returns null if it's not found.
    /// Will return the first matched item so if you combine flags only one of them will be returned
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public Holiday? GetHoliday(HolidayType type)
    {
        return GetHolidays().FirstOrDefault(x => x.Type == type);
    }

    public IEnumerable<Holiday> GetSpecificHolidays(HolidayType type)
    {
        return GetHolidays().Where(x => type.HasFlag(x.Type));
    }

}


public delegate HolidayCollection HolidayDayCachingDelegate(int year, Func<int, HolidayCollection> holidayRetriever);

public static class DanishHolidays
{
    static DanishHolidays()
    {
        _yearCollection =  new ConcurrentDictionary<int, HolidayCollection>();
       ResetCacheDelegate();
    }

    internal static HolidayDayCachingDelegate CacheDelegate;

    public static readonly ConcurrentDictionary<int, HolidayCollection> _yearCollection;
       

    public static HolidayCollection GetCollectionForYear(int year)
    {
        return CacheDelegate(year, y => new HolidayCollection(GetAllHolidays(y).OrderBy(x => x.Month).ThenBy(x => x.Day), y));
    }

    internal static IEnumerable<Holiday> GetAllHolidays(int year)
    {
        var easterSunday = EasterUtil.FindEasterSunday(year);
        var easterMonday = easterSunday.AddDays(1);
        var maundyThursday = easterSunday.AddDays(-3);
        var goodFriday = easterSunday.AddDays(-2);
        var ascensionDay = easterSunday.AddDays(39);
        var whitSunday = easterSunday.AddDays(49);
        var whitMonday = easterSunday.AddDays(50);
        var greatPrayerDay = easterSunday.AddDays(26);


        yield return new(year, 1, 1, HolidayType.Nytaarsdag);
        yield return new(year, 12, 24, HolidayType.Juleaftensdag);
        yield return new(year, 12, 25, HolidayType.FoersteJuledag);
        yield return new(year, 12, 26, HolidayType.AndenJuledag);
        yield return new(year, 12, 31, HolidayType.Nytaarsaften);
        yield return new(easterSunday.Year, easterSunday.Month, easterSunday.Day, HolidayType.PaaskeSoendag);
        yield return new(easterMonday.Year, easterMonday.Month, easterMonday.Day, HolidayType.AndenPaaskedag);
        yield return new(maundyThursday.Year, maundyThursday.Month, maundyThursday.Day, HolidayType.SkaerTorsdag);
        yield return new(goodFriday.Year, goodFriday.Month, goodFriday.Day, HolidayType.LangFredag);
        yield return new(ascensionDay.Year, ascensionDay.Month, ascensionDay.Day, HolidayType.KristiHimmelfartsdag);
        yield return new(whitSunday.Year, whitSunday.Month, whitSunday.Day, HolidayType.PinseSoendag);
        yield return new(whitMonday.Year, whitMonday.Month, whitMonday.Day, HolidayType.AndenPinsedag);
        yield return new(year, 6, 5, HolidayType.Grundlovsdag);
        yield return new(greatPrayerDay.Year, greatPrayerDay.Month, greatPrayerDay.Day, HolidayType.StoreBededag);
        yield return new(year, 5, 1, HolidayType.FoersteMaj);

    }

    internal static void ResetCacheDelegate()
    {
        CacheDelegate = _yearCollection.GetOrAdd;
    }
}