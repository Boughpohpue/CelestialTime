namespace CelestialTime;

public static class DateTimeSolarExtensions
{
    public const double EclipseToleranceDays = 0.69;

    public static DateTime GetSunrise(this DateTime dt, double latitude, double longitude)
    {
        var sunEventTime = dt.CalculateSunEvent(latitude, longitude, SolarEvent.Sunrise);
        if (sunEventTime.Date > dt.Date)
        {
            sunEventTime = dt.AddDays(-1).CalculateSunEvent(latitude, longitude, SolarEvent.Sunrise);
        }
        return sunEventTime;
    }
    public static DateTime GetSunset(this DateTime dt, double latitude, double longitude)
    {
        var sunEventTime = dt.CalculateSunEvent(latitude, longitude, SolarEvent.Sunset);
        if (sunEventTime.Date < dt.Date)
        {
            sunEventTime = dt.AddDays(1).CalculateSunEvent(latitude, longitude, SolarEvent.Sunset);
        }
        return sunEventTime;
    }
    public static bool IsSolarEclipse(this DateTime dt)
    {
        return dt.GetMoonPhase() == LunarPhase.NewMoon
            && EclipseHelper.IsNearMoonNode(dt, EclipseToleranceDays);
    }
    public static DateTime GetLastSolarEclipse(this DateTime dt)
    {
        var eclipseDate = dt.Date;
        while (!eclipseDate.IsSolarEclipse())
        {
            eclipseDate = eclipseDate.AddDays(-1);
        }
        return eclipseDate;
    }
    public static DateTime GetNextSolarEclipse(this DateTime dt)
    {
        var eclipseDate = dt.Date;
        while (!eclipseDate.IsSolarEclipse())
        {
            eclipseDate = eclipseDate.AddDays(1);
        }
        return eclipseDate;
    }

    private static DateTime CalculateSunEvent(this DateTime date, double latitude, double longitude, SolarEvent sunEvent)
    {
        // Get approximate time
        double lngHour = longitude / 15.0;
        double t = date.DayOfYear + (((int)sunEvent) - lngHour) / 24.0;

        // Get Sun mean anomaly
        double M = (0.9856 * t) - 3.289;

        // Get Sun true longitude
        double L = M + (1.916 * Math.Sin(Angle.Deg2Rad(M))) + (0.020 * Math.Sin(2 * Angle.Deg2Rad(M))) + 282.634;
        L = Angle.Normalize(L);

        // Get Sun right ascension
        double RA = Angle.Rad2Deg(Math.Atan(0.91764 * Math.Tan(Angle.Deg2Rad(L))));
        RA = Angle.Normalize(RA);

        // Adjust RA to correct quadrant
        double Lquadrant = Math.Floor(L / 90) * 90;
        double RAquadrant = Math.Floor(RA / 90) * 90;
        RA = RA + (Lquadrant - RAquadrant);
        RA /= 15.0;

        // Get Sun declination
        double sinDec = 0.39782 * Math.Sin(Angle.Deg2Rad(L));
        double cosDec = Math.Cos(Math.Asin(sinDec));

        // Get Sun local hour angle
        double cosH = (Math.Cos(Angle.Deg2Rad(90.833)) - (sinDec * Math.Sin(Angle.Deg2Rad(latitude)))) /
                      (cosDec * Math.Cos(Angle.Deg2Rad(latitude)));

        if (cosH > 1) return DateTime.MinValue;  // Sun never rises
        if (cosH < -1) return DateTime.MaxValue; // Sun never sets

        double H = sunEvent == SolarEvent.Sunrise
            ? 360 - Angle.Rad2Deg(Math.Acos(cosH))
            : Angle.Rad2Deg(Math.Acos(cosH));
        H /= 15.0;

        // Get local mean time
        double T = H + RA - (0.06571 * t) - 6.622;

        // Get UTC time
        double UT = T - lngHour;
        UT = (UT + 24) % 24;

        // Convert to local time
        TimeZoneInfo tz = TimeZoneHelper.GetTimeZoneInfo(latitude, longitude);
        DateTime utcDate = new DateTime(date.Year, date.Month, date.Day).AddHours(UT);
        DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcDate, tz);
        return localTime;
    }
}
