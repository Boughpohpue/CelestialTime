namespace CelestialTime;

public static class EclipseHelper
{
    public const double NodeScoreWeight = 1.0;
    public const double FullMoonScoreWeight = 3.0;
    public const double DraconicMonth = 27.212220;

    public static double GetMoonNodeAge(DateTime dt)
    {
        // Get fractional position of Moon along node cycle (0 = ascending node)
        double nodePhase = ((dt - MoonInfo.KnownAscendingNodeDate).TotalDays / DraconicMonth) % 1.0;

        if (nodePhase < 0) nodePhase += 1.0;

        return Math.Min(nodePhase, 1.0 - nodePhase) * DraconicMonth;
    }
    public static bool IsNearMoonNode(DateTime dt, double toleranceDays)
    {
        var nodeAge = GetMoonNodeAge(dt);
        return nodeAge < toleranceDays
            || Math.Abs(nodeAge - DraconicMonth / 2) < toleranceDays;
    }
    public static double GetNodeScore(DateTime dt, double weight = NodeScoreWeight)
    {
        double nodeAge = GetMoonNodeAge(dt);
        return Math.Min(nodeAge, Math.Abs(nodeAge - DraconicMonth / 2)) * weight;
    }
    public static double GetFullMoonScore(DateTime dt, double weight = FullMoonScoreWeight)
    {
        return Math.Abs(dt.GetMoonAge() - MoonInfo.MaxAge / 2) * weight;
    }
    public static double GetEclipseScore(DateTime dt)
    {
        return GetFullMoonScore(dt) + GetNodeScore(dt);
    }
    public static double GetEclipseTopScore(DateTime dt)
    {
        var date = dt.Date;
        var topTime = date;
        double score = GetEclipseScore(date);
        for (var h = 0.0; h < 24.0; h += 0.5)
        {
            var testTime = date.AddHours(h);
            var testScore = GetEclipseScore(testTime);
            if (testScore < score)
            {
                score = testScore;
                topTime = testTime;
            }
        }
        return score;
    }
    public static DateTime AdjustEclipseHour(DateTime dt)
    {
        var eclipseTime = dt.AddHours(-12);
        var eclipseTimeScore = GetEclipseScore(eclipseTime);
        for (var h = -11; h < 24; h++)
        {
            var testTime = dt.AddHours(h);
            var testTimeScore = GetEclipseScore(testTime);
            if (testTimeScore < eclipseTimeScore)
            {
                eclipseTime = testTime;
                eclipseTimeScore = testTimeScore;
            }
        }
        return AdjustEclipseMinute(eclipseTime);
    }
    public static DateTime AdjustEclipseMinute(DateTime dt)
    {
        var eclipseTime = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0);
        var eclipseTimeScore = GetEclipseScore(eclipseTime);
        for (var m = 1; m < 60; m++)
        {
            var testTime = dt.AddMinutes(m);
            var testTimeScore = GetEclipseScore(testTime);
            if (testTimeScore < eclipseTimeScore)
            {
                eclipseTime = testTime;
                eclipseTimeScore = testTimeScore;
            }
        }
        return eclipseTime;
    }
}
