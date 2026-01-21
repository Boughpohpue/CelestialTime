using System.Text;

namespace CelestialTime;

public class CelestialMoment
{
    public PlaceInfo PlaceInfo { get; init; }
    public SolInfo SunInfo { get; init; }
    public LunaInfo MoonInfo { get; init; }

    public CelestialMoment(DateTime dt, PlaceInfo placeInfo)
    {
        PlaceInfo = placeInfo;
        SunInfo = new SolInfo(dt, placeInfo.Latitude, placeInfo.Longitude);
        MoonInfo = new LunaInfo(dt);
    }
    public CelestialMoment(DateTime dt, double latitude = 0.0, double longitude = 0.0, string placeName = "")
        : this(dt, new PlaceInfo(latitude, longitude, placeName)) { }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{string.Join("", Enumerable.Repeat("=", 69))}");
        sb.AppendLine();
        sb.AppendLine(PlaceInfo.ToString());
        sb.AppendLine(SunInfo.ToString());
        sb.AppendLine(MoonInfo.ToString());
        sb.AppendLine($"{string.Join("", Enumerable.Repeat("=", 69))}");
        return sb.ToString();
    }
}
