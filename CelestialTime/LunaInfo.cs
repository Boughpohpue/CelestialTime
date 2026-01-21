using System.Text;

namespace CelestialTime;

public class LunaInfo
{
    public const double MaxAge = 29.53059;
    public const double NodeRegressionPerDay = 360 / 6798.383; //deg/day
    public static readonly TimeSpan MaxAgeTS = TimeSpan.FromDays(MaxAge);
    public static readonly DateTime NewMoonKnownDate = new DateTime(2012, 12, 13, 9, 42, 12);
    public static readonly DateTime AscendingNodeKnownDate = new DateTime(2007, 3, 3, 23, 20, 54);

    public double Age { get; init; }
    public double AgePercent { get; init; }
    public double PhasePercent { get; init; }
    public double Illumination { get; init; }
    public bool IsEclipse { get; init; }
    public LunarPhase Phase { get; init; }
    public DateTime InfoDate { get; init; }
    public DateTime NextNewMoon { get; init; }
    public DateTime LastNewMoon { get; init; }
    public DateTime NextFullMoon { get; init; }
    public DateTime LastFullMoon { get; init; }
    public DateTime NextEclipse { get; init; }
    public DateTime LastEclipse { get; init; }

    public TimeSpan AgeTS => TimeSpan.FromDays(Age); 
    public TimeSpan ToNextNewMoon => NextNewMoon - InfoDate; 
    public TimeSpan SinceLastNewMoon => InfoDate - LastNewMoon; 
    public TimeSpan ToNextFullMoon => NextFullMoon - InfoDate; 
    public TimeSpan SinceLastFullMoon => InfoDate - LastFullMoon; 
    public TimeSpan ToNextEclipse => NextEclipse - InfoDate; 
    public TimeSpan SinceLastEclipse => InfoDate - LastEclipse; 

    public LunaInfo(DateTime dt)
    {
        this.InfoDate = dt;
        this.Age = dt.GetMoonAge();
        this.Phase = dt.GetMoonPhase();
        this.AgePercent = dt.GetMoonAgePercent();
        this.PhasePercent = dt.GetMoonPhasePercent();
        this.Illumination = dt.GetMoonIllumination();
        this.IsEclipse = dt.IsLunarEclipse();
        this.NextNewMoon = dt.GetNextNewMoon();
        this.LastNewMoon = dt.GetLastNewMoon();
        this.NextFullMoon = dt.GetNextFullMoon();
        this.LastFullMoon = dt.GetLastFullMoon();
        this.NextEclipse = dt.GetNextLunarEclipse();
        this.LastEclipse = dt.GetLastLunarEclipse();
    }
    public LunaInfo(int year, int month, int day) : this(new DateTime(year, month, day)) { }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine("MOON INFO:");
        if (IsEclipse) sb.AppendLine("ECLIPSE!");
        sb.AppendLine($"Age:            {AgeTS.ToNiceString()}");
        sb.AppendLine($"Phase:          {Phase.ToString().ToSentence()}");
        sb.AppendLine($"Percent:        {Math.Round(AgePercent * 100.0, 2)}%");
        sb.AppendLine($"Phase percent:  {Math.Round(PhasePercent * 100.0, 2)}%");
        sb.AppendLine($"Illumination:   {Math.Round(Illumination * 100.0, 2)}%");
        sb.AppendLine($"Last new moon:  {LastNewMoon}, {SinceLastNewMoon.ToNiceString()} ago");
        sb.AppendLine($"Last full moon: {LastFullMoon}, {SinceLastFullMoon.ToNiceString()} ago");
        sb.AppendLine($"Next new moon:  {NextNewMoon}, in {ToNextNewMoon.ToNiceString()}");
        sb.AppendLine($"Next full moon: {NextFullMoon}, in {ToNextFullMoon.ToNiceString()}");
        sb.AppendLine($"Last eclipse:   {LastEclipse.ToShortDateString()}, {SinceLastEclipse.ToNiceString()} ago");
        sb.AppendLine($"Next eclipse:   {NextEclipse.ToShortDateString()}, in {ToNextEclipse.ToNiceString()}");
        return sb.ToString();
    }
}
