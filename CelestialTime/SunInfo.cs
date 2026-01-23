using Infertus.Geo.Primitives;
using System.Text;

namespace CelestialTime;

public class SunInfo
{
    public DateTime InfoDate { get; init; }
    public Coordinates Coordinates { get; init; }
    public bool IsInEclipse { get; init; }
    public DateTime NextEclipse { get; init; }
    public DateTime LastEclipse { get; init; }
    public DateTime SunriseTime { get; init; }
    public DateTime SunsetTime { get; init; }


    public SunInfo(DateTime dt, Coordinates coordinates)
    {
        InfoDate = dt;
        Coordinates = coordinates;
        IsInEclipse = dt.IsSolarEclipse();
        LastEclipse = dt.GetLastSolarEclipse();
        NextEclipse = dt.GetNextSolarEclipse();
        SunriseTime = dt.GetSunriseTime(Coordinates.Lat, Coordinates.Lon);
        SunsetTime = dt.GetSunsetTime(Coordinates.Lat, Coordinates.Lon);
    }

    public SunInfo(DateTime dt)
        : this(dt, new Coordinates(0.0, 0.0)) { }

    public SunInfo(DateTime dt, double latitude, double longitude)
        : this(dt, new Coordinates(latitude, longitude)) { }

    public SunInfo(int year, int month, int day, Coordinates coordinates)
        : this(new DateTime(year, month, day), coordinates) { }

    public SunInfo(int year, int month, int day, double latitude, double longitude)
        : this(new DateTime(year, month, day), new Coordinates(latitude, longitude)) { }


    public int DayOfYear => LocalTime.DayOfYear;
    public bool IsBeforeSunrise => LocalTime < SunriseTime;
    public bool IsAfterSunrise => LocalTime > SunriseTime;
    public bool IsBeforeSunset => LocalTime < SunsetTime;
    public bool IsAfterSunset => LocalTime > SunsetTime;
    public TimeSpan DayLength => SunsetTime - SunriseTime;
    public TimeSpan NightLength => TimeSpan.FromDays(1) - DayLength;
    public TimeSpan ToNextEclipse => NextEclipse - LocalTime;
    public TimeSpan SinceLastEclipse => LocalTime - LastEclipse;
    public TimeSpan ToNextSunrise => (IsBeforeSunrise ? SunriseTime : LocalTime.AddDays(1).GetSunriseTime(Coordinates.Lat, Coordinates.Lon)) - LocalTime;
    public TimeSpan SinceLastSunrise => LocalTime - (IsBeforeSunrise ? LocalTime.AddDays(-1).GetSunriseTime(Coordinates.Lat, Coordinates.Lon) : SunriseTime);
    public TimeSpan ToNextSunset => (IsBeforeSunset ? SunsetTime : LocalTime.AddDays(1).GetSunsetTime(Coordinates.Lat, Coordinates.Lon)) - LocalTime;
    public TimeSpan SinceLastSunset => LocalTime - (IsBeforeSunset ? LocalTime.AddDays(-1).GetSunsetTime(Coordinates.Lat, Coordinates.Lon) : SunsetTime);
    public DateTime LocalTime => TimeZoneInfo.ConvertTimeFromUtc(InfoDate.ToUniversalTime(), TimeZoneHelper.GetTimeZoneInfo(Coordinates.Lat, Coordinates.Lon));
    public TimeSpan TimeDifference => LocalTime - InfoDate;


    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine("SUN INFO:");
        if (IsInEclipse) sb.AppendLine("ECLIPSE!");
        sb.AppendLine($"Info time:      {InfoDate}");
        sb.AppendLine($"Local time:     {LocalTime}");
        sb.AppendLine($"Day of year:    {DayOfYear}");
        sb.AppendLine($"Day length:     {DayLength.ToNiceString()}");
        sb.AppendLine($"Night length:   {NightLength.ToNiceString()}");
        sb.AppendLine($"Sunrise time:   {SunriseTime.ToShortTimeString()}");
        sb.AppendLine($"Sunset time:    {SunsetTime.ToShortTimeString()}");
        sb.AppendLine($"Last sunrise:   {SinceLastSunrise.ToNiceString()}");
        sb.AppendLine($"Next sunrise:   {ToNextSunrise.ToNiceString()}");
        sb.AppendLine($"Last sunset:    {SinceLastSunset.ToNiceString()}");
        sb.AppendLine($"Next sunset:    {ToNextSunset.ToNiceString()}");
        sb.AppendLine($"Last eclipse:   {LastEclipse.ToShortDateString()}, {(int)SinceLastEclipse.TotalDays}d ago");
        sb.AppendLine($"Next eclipse:   {NextEclipse.ToShortDateString()}, in {(int)ToNextEclipse.TotalDays}d");
        return sb.ToString();
    }
}
