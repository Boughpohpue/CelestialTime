using GeoTimeZone;
using TimeZoneConverter;

namespace CelestialTime;

public static class TimeZoneHelper
{
    public static TimeZoneInfo GetTimeZoneInfo(double latitude, double longitude)
    {
        try
        {
            return TZConvert.GetTimeZoneInfo(TimeZoneLookup.GetTimeZone(latitude, longitude).Result);
        }
        catch
        {
            return TimeZoneInfo.Local;
        }
    }
}
