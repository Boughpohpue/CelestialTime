namespace CelestialTime;

public static class Angle
{
    public const double ZERO = 0.0;
    public const double FULL = 360.0;
    public const double HALF = FULL / 2;
    public const double RIGHT = HALF / 2;
    public const double RAD2DEG = HALF / Math.PI;
    public const double DEG2RAD = Math.PI / HALF;

    public static double Rad2Deg(double rad) => RAD2DEG * rad;
    public static double Deg2Rad(double deg) => DEG2RAD * Normalize(deg);
    public static double Normalize(double deg) => ((deg % FULL) + FULL) % FULL;
}
