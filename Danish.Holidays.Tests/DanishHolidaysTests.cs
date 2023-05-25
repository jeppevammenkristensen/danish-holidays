using FluentAssertions;

namespace Danish.Holidays.Tests;


public class DanishHolidaysTests : IDisposable
{
    public DanishHolidaysTests()
    {
        DanishHolidays.CacheDelegate = (year, retriever) => retriever(year);
    }

    [Fact]
    public void AllHolidays_Before2023_ReturnsCorrectCollection()
    {
        var result = DanishHolidays.GetCollectionForYear(2023);
        result.Should().NotBeNull();
        var holidays = result.AllHolidays.Should().HaveCount(15).And.Subject.ToList();

        var allDays = new List<Holiday>();

        allDays.Add(new Holiday(2023, 1, 1, HolidayType.Nytaarsdag));
        allDays.Add(new Holiday(2023, 4, 6, HolidayType.SkaerTorsdag));
        allDays.Add(new Holiday(2023, 4, 7, HolidayType.LangFredag));
        allDays.Add(new Holiday(2023, 4, 9, HolidayType.PaaskeSoendag));
        allDays.Add(new Holiday(2023, 4, 10, HolidayType.AndenPaaskedag));
        allDays.Add(new Holiday(2023, 5, 1, HolidayType.FoersteMaj));
        allDays.Add(new Holiday(2023, 5, 5, HolidayType.StoreBededag));
        allDays.Add(new Holiday(2023, 5, 18, HolidayType.KristiHimmelfartsdag));
        allDays.Add(new Holiday(2023, 5, 28, HolidayType.PinseSoendag));
        allDays.Add(new Holiday(2023, 5, 29, HolidayType.AndenPinsedag));
        allDays.Add(new Holiday(2023, 6, 5, HolidayType.Grundlovsdag));
        allDays.Add(new Holiday(2023, 12, 24, HolidayType.Juleaftensdag));
        allDays.Add(new Holiday(2023, 12, 25, HolidayType.FoersteJuledag));
        allDays.Add(new Holiday(2023, 12, 26, HolidayType.AndenJuledag));
        allDays.Add(new Holiday(2023, 12, 31, HolidayType.Nytaarsaften));

        holidays.Should().BeEquivalentTo(allDays);
    }


    public void Dispose()
    {
        DanishHolidays.ResetCacheDelegate();
    }
}