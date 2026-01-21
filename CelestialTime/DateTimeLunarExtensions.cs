namespace CelestialTime;

public static class DateTimeLunarExtensions
{
    public const double EclipseMaxScore = 1.69;
    public const double EclipseToleranceDays = 1.5;
    public const double MoonPhaseToleranceDays = 3;
    
    public static double GetMoonAge(this DateTime dt)
    {
        return (dt - LunaInfo.NewMoonKnownDate).TotalDays % LunaInfo.MaxAge;
    }
    public static double GetMoonAgePercent(this DateTime dt)
    {
        return ((dt - LunaInfo.NewMoonKnownDate).TotalDays % LunaInfo.MaxAge) / LunaInfo.MaxAge;
    }
    public static double GetMoonIllumination(this DateTime dt)
    {
        return (dt.GetMoonAge() - (LunaInfo.MaxAge / 2)) / LunaInfo.MaxAge;
    }
    public static LunarPhase GetMoonPhase(this DateTime dt)
    {
        const double segment = 1.0 / 8.0;
        const double halfSegment = segment / 2.0;
        var p = (dt.GetMoonAgePercent() % 1 + 1) % 1;
        var phase = LunarPhase.NewMoon;
        foreach (LunarPhase lp in Enum.GetValues(typeof(LunarPhase)))
        {
            phase = lp;
            if (p >= 1 - halfSegment || p < halfSegment) break;
            if (p < (segment * (int)phase) + halfSegment) break;
        }
        return phase;
    }
    public static double GetMoonPhasePercent(this DateTime dt)
    {
        var phaseSegment = 1.0 / 8.0;
        var phaseHalfSegment = phaseSegment / 2.0;
        var phaseSegmentDuration = phaseSegment * LunaInfo.MaxAge;
        var phaseHalfSegmentDuration = phaseHalfSegment * LunaInfo.MaxAge;

        var age = dt.GetMoonAge() + phaseHalfSegmentDuration;
        if (age > LunaInfo.MaxAge)
        {
            age -= LunaInfo.MaxAge;
        }
        while (age > phaseSegmentDuration)
        {
            age -= phaseSegmentDuration;
        }
        if (age < phaseHalfSegmentDuration)
        {
            return 1.0 - (age / 2 * phaseHalfSegmentDuration);
        }
        else
        {
            return (age / 2 * phaseHalfSegmentDuration);
        }
    }
    public static DateTime GetNextNewMoon(this DateTime dt)
    {
        ;
        return dt.AddDays(LunaInfo.MaxAge - dt.GetMoonAge());
    }
    public static DateTime GetNextFullMoon(this DateTime dt)
    {
        var age = dt.GetMoonAge();
        return age < LunaInfo.MaxAge / 2
            ? dt.AddDays((LunaInfo.MaxAge / 2) - age)
            : dt.AddDays(LunaInfo.MaxAge).AddDays((LunaInfo.MaxAge / 2) - age);
    }
    public static DateTime GetLastNewMoon(this DateTime dt)
    {
        ;
        return dt.AddDays(dt.GetMoonAge() * -1);
    }
    public static DateTime GetLastFullMoon(this DateTime dt)
    {
        var age = dt.GetMoonAge();
        return age > LunaInfo.MaxAge / 2
            ? dt.AddDays((LunaInfo.MaxAge / 2) - age)
            : dt.AddDays(-LunaInfo.MaxAge).AddDays((LunaInfo.MaxAge / 2) - age);
    }
    public static DateTime GetNextMoonPhase(this DateTime dt, LunarPhase phase)
    {
        var age = dt.GetMoonAge();
        var targetPhaseAge = (int)phase * LunaInfo.MaxAge / 8.0;
        return age < targetPhaseAge
            ? dt.AddDays(targetPhaseAge - age)
            : dt.AddDays(LunaInfo.MaxAge - (age - targetPhaseAge));
    }
    public static DateTime GetLastMoonPhase(this DateTime dt, LunarPhase phase)
    {
        var age = dt.GetMoonAge();
        var targetPhaseAge = (int)phase * LunaInfo.MaxAge / 8.0;
        return age > targetPhaseAge
            ? dt.AddDays(targetPhaseAge - age)
            : dt.AddDays((targetPhaseAge - age) - LunaInfo.MaxAge);
    }
    public static bool IsNearNewMoon(this DateTime dt)
    {
        var age = dt.GetMoonAge();
        var min = LunaInfo.MaxAge - MoonPhaseToleranceDays;
        var max = MoonPhaseToleranceDays;
        return age <= max || age >= min;
    }
    public static bool IsNearFullMoon(this DateTime dt)
    {
        var age = dt.GetMoonAge();
        var min = LunaInfo.MaxAge / 2 - MoonPhaseToleranceDays;
        var max = LunaInfo.MaxAge / 2 + MoonPhaseToleranceDays;
        return age >= min && age <= max;
    }
    public static bool IsLunarEclipse(this DateTime dt)
    {
        return dt.IsLunarEclipsePrecise();
        return dt.IsNearFullMoon()
            && EclipseHelper.IsNearMoonNode(dt, EclipseToleranceDays)
            && EclipseHelper.GetEclipseTopScore(dt) <= EclipseMaxScore;
    }
    public static bool IsLunarEclipsePrecise(this DateTime dt)
    {
        var date = dt.Date;
        if (!date.IsNearFullMoon() || !EclipseHelper.IsNearMoonNode(date, EclipseToleranceDays))
            return false;

        var dtScore = EclipseHelper.GetEclipseTopScore(date);
        if (dtScore > 1.69)
            return false;

        var nextDayScore = EclipseHelper.GetEclipseTopScore(date.AddDays(1));
        var prevDayScore = EclipseHelper.GetEclipseTopScore(date.AddDays(-1));

        return dtScore < nextDayScore && dtScore < prevDayScore;
    }
    public static DateTime GetLastLunarEclipse(this DateTime dt)
    {
        var eclipseDate = dt.Date;
        while (!eclipseDate.IsLunarEclipse())
        {
            eclipseDate = eclipseDate.AddDays(-1);
        }
        return eclipseDate;
    }
    public static DateTime GetNextLunarEclipse(this DateTime dt)
    {
        var eclipseDate = dt.Date;
        while (!eclipseDate.IsLunarEclipse())
        {
            eclipseDate = eclipseDate.AddDays(1);
        }
        return eclipseDate;
    }
}
