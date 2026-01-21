namespace CelestialTime;

public static class Angle
{
    public const double ZERO = 0.0;
    public const double FULL = 360.0;
    public const double HALF = FULL / 2;
    public const double RIGHT = FULL / 4;
    public const double RAD2DEG = HALF / Math.PI;
    public const double DEG2RAD = Math.PI / HALF;

    public static double Normalize(double deg)
    {
        return deg >= 0
            ? deg % FULL
            : (deg % FULL) + FULL;
    }
    public static double Deg2Rad(double deg)
    {
        return DEG2RAD * Normalize(deg);
    }
    public static double Rad2Deg(double rad)
    {
        return RAD2DEG * rad;
    }
    public static string DegToDms(double value, char positive, char negative)
    {
        var direction = value >= 0 ? positive : negative;

        value = Math.Abs(value);

        int degrees = (int)value;
        double minutesFull = (value - degrees) * 60;
        int minutes = (int)minutesFull;
        int seconds = (int)Math.Round((minutesFull - minutes) * 60);

        if (seconds == 60)
        {
            seconds = 0;
            minutes++;
        }
        if (minutes == 60)
        {
            minutes = 0;
            degrees++;
        }

        return $"{degrees}°{minutes:00}'{seconds:00}\"{direction}";
    }

}
