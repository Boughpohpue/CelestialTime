namespace CelestialTime;

public static class TimeSpanExtensions
{
    public static string ToNiceString(this TimeSpan ts, bool withSeconds = false)
    {
        var retval = string.Empty;
        if (ts.Seconds > 0 && withSeconds)
            retval = string.Format("{0}s", ts.Seconds);
        if (ts.Minutes > 0)
            retval = retval.Length == 0 ? string.Format("{0}m", ts.Minutes) : string.Format("{0}m {1}", ts.Minutes, retval);
        if (ts.Hours > 0)
            retval = retval.Length == 0 ? string.Format("{0}h", ts.Hours) : string.Format("{0}h {1}", ts.Hours, retval);
        if (ts.Days > 0)
            retval = retval.Length == 0 ? string.Format("{0}d", ts.Days) : string.Format("{0}d {1}", ts.Days, retval);
        return retval;
    }
}
