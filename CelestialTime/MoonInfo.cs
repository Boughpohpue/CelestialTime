using System.Text;

namespace CelestialTime;

public class MoonInfo
{
    public const double MaxAge = 29.53059;
    public const double NodeRegressionPerDay = 360 / 6798.383;
    public static readonly TimeSpan MaxAgeTS = TimeSpan.FromDays(MaxAge);
    public static readonly DateTime KnownNewMoonDate = new DateTime(2012, 12, 13, 9, 42, 12);
    public static readonly DateTime KnownAscendingNodeDate = new DateTime(2007, 3, 3, 23, 20, 54);

    public DateTime InfoDate { get; init; }
    public double Age { get; init; }
    public double AgePercent { get; init; }
    public double PhasePercent { get; init; }
    public double Illumination { get; init; }
    public bool IsInEclipse { get; init; }
    public MoonPhase Phase { get; init; }
    public DateTime NextNewMoon { get; init; }
    public DateTime LastNewMoon { get; init; }
    public DateTime NextFullMoon { get; init; }
    public DateTime LastFullMoon { get; init; }
    public DateTime NextEclipse { get; init; }
    public DateTime LastEclipse { get; init; }


    public MoonInfo(DateTime dt)
    {
        InfoDate = dt;
        Age = dt.GetMoonAge();
        Phase = dt.GetMoonPhase();
        AgePercent = dt.GetMoonAgePercent();
        Illumination = dt.GetMoonIllumination();
        IsInEclipse = dt.IsLunarEclipse();
        NextNewMoon = dt.GetNextNewMoon();
        LastNewMoon = dt.GetLastNewMoon();
        NextFullMoon = dt.GetNextFullMoon();
        LastFullMoon = dt.GetLastFullMoon();
        NextEclipse = dt.GetNextLunarEclipse();
        LastEclipse = dt.GetLastLunarEclipse();
    }

    public MoonInfo(int year, int month, int day) 
        : this(new DateTime(year, month, day)) { }


    public TimeSpan AgeTS => TimeSpan.FromDays(Age);
    public TimeSpan ToNextNewMoon => NextNewMoon - InfoDate;
    public TimeSpan SinceLastNewMoon => InfoDate - LastNewMoon;
    public TimeSpan ToNextFullMoon => NextFullMoon - InfoDate;
    public TimeSpan SinceLastFullMoon => InfoDate - LastFullMoon;
    public TimeSpan ToNextEclipse => NextEclipse - InfoDate;
    public TimeSpan SinceLastEclipse => InfoDate - LastEclipse;


    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine("MOON INFO:");
        if (IsInEclipse) sb.AppendLine("ECLIPSE!");
        sb.AppendLine($"Info time:      {InfoDate}");
        sb.AppendLine($"Age:            {AgeTS.ToNiceString()} ({Math.Round(AgePercent * 100.0, 2)}%)");
        sb.AppendLine($"Phase:          {Phase.ToString().ToSentence()}");
        sb.AppendLine($"Illumination:   {Math.Round(Illumination * 100.0, 2)}%");
        sb.AppendLine($"Last new moon:  {LastNewMoon}, {SinceLastNewMoon.ToNiceString()} ago");
        sb.AppendLine($"Next new moon:  {NextNewMoon}, in {ToNextNewMoon.ToNiceString()}");
        sb.AppendLine($"Last full moon: {LastFullMoon}, {SinceLastFullMoon.ToNiceString()} ago");
        sb.AppendLine($"Next full moon: {NextFullMoon}, in {ToNextFullMoon.ToNiceString()}");
        sb.AppendLine($"Last eclipse:   {LastEclipse.ToShortDateString()}, {SinceLastEclipse.ToNiceString()} ago");
        sb.AppendLine($"Next eclipse:   {NextEclipse.ToShortDateString()}, in {ToNextEclipse.ToNiceString()}");
        return sb.ToString();
    }
}
