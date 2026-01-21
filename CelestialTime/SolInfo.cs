using System.Text;

namespace CelestialTime;

public class SolInfo
{
    public bool IsEclipse { get; private set; }
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }
    public DateTime InfoDate { get; private set; }
    public DateTime Sunrise { get; private set; }
    public DateTime Sunset { get; private set; }
    public DateTime NextEclipse { get; private set; }
    public DateTime LastEclipse { get; private set; }

    public int DayOfYear => InfoDate.DayOfYear; 
    public bool IsAfterSunrise => LocalTime > Sunrise; 
    public bool IsBeforeSunrise => !IsAfterSunrise; 
    public bool IsAfterSunset => LocalTime > Sunset; 
    public bool IsBeforeSunset => !IsAfterSunset; 
    public TimeSpan DayLength => Sunset - Sunrise; 
    public TimeSpan NightLength => TimeSpan.FromDays(1) - DayLength;
    public DateTime LocalTime => TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneHelper.GetTimeZoneInfo(Latitude, Longitude));
    public TimeSpan ToNextSunset => (IsBeforeSunset ? Sunset : LocalTime.AddDays(1).GetSunset(Latitude, Longitude)) - LocalTime; 
    public TimeSpan SinceLastSunset => LocalTime - (IsBeforeSunset ? LocalTime.AddDays(-1).GetSunset(Latitude, Longitude) : Sunset); 
    public TimeSpan ToNextSunrise => (IsBeforeSunrise ? Sunrise : LocalTime.AddDays(1).GetSunrise(Latitude, Longitude)) - LocalTime; 
    public TimeSpan SinceLastSunrise => LocalTime - (IsBeforeSunrise ? LocalTime.AddDays(-1).GetSunrise(Latitude, Longitude) : Sunrise); 
    public TimeSpan ToNextEclipse => NextEclipse - LocalTime; 
    public TimeSpan SinceLastEclipse => LocalTime - LastEclipse; 
    

    public SolInfo(DateTime dt, double latitude, double longitude)
    {
        this.InfoDate = dt;
        this.Latitude = latitude;
        this.Longitude = longitude;
        this.Sunrise = dt.GetSunrise(latitude, longitude);
        this.Sunset = dt.GetSunset(latitude, longitude);
        this.IsEclipse = dt.IsSolarEclipse();
        this.LastEclipse = dt.GetLastSolarEclipse();
        this.NextEclipse = dt.GetNextSolarEclipse();
    }
    public SolInfo(int year, int month, int day, double latitude, double longitude)
        : this(new DateTime(year, month, day), latitude, longitude) { }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine("SUN INFO:");
        if (IsEclipse) sb.AppendLine("ECLIPSE!");
        sb.AppendLine($"Local time:     {LocalTime}");
        sb.AppendLine($"Day of year:    {DayOfYear}");
        sb.AppendLine($"Day length:     {DayLength.ToNiceString()}");
        sb.AppendLine($"Night length:   {NightLength.ToNiceString()}");
        sb.AppendLine($"Sunrise:        {Sunrise.ToShortTimeString()}");
        sb.AppendLine($"Sunset:         {Sunset.ToShortTimeString()}");
        sb.AppendLine($"Last sunrise:   {SinceLastSunrise.ToNiceString()}");
        sb.AppendLine($"Last sunset:    {SinceLastSunset.ToNiceString()}");
        sb.AppendLine($"Next sunrise:   {ToNextSunrise.ToNiceString()}");
        sb.AppendLine($"Next sunset:    {ToNextSunset.ToNiceString()}");
        sb.AppendLine($"Last eclipse:   {LastEclipse.ToShortDateString()}, {(int)SinceLastEclipse.TotalDays}d ago");
        sb.AppendLine($"Next eclipse:   {NextEclipse.ToShortDateString()}, in {(int)ToNextEclipse.TotalDays}d");
        return sb.ToString();
    }
}
