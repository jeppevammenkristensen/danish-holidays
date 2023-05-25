using FluentAssertions;

namespace Danish.Holidays.Tests;

public class EasterUtilTests
{
    [Theory]
    [InlineData(2008,03,23)]
    [InlineData(2023,04,09)]
    [InlineData(2024,03,31)]
    [InlineData(2025,04,20)]
    public void FindEasterSunday_Year_MatchesDate(int year, int expectedMonth, int expectedDay)
    {
        var date = EasterUtil.FindEasterSunday(year);
        
        date.Year.Should().Be(year);
        date.Month.Should().Be(expectedMonth);
        date.Day.Should().Be(expectedDay);
    }
}