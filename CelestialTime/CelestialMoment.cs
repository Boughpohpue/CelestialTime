using Infertus.Geo.Primitives;
using System.Text;

namespace CelestialTime;

public class CelestialMoment
{
    public DateTime Moment { get; init; }
    public SunInfo SunInfo { get; init; }
    public MoonInfo MoonInfo { get; init; }
    public PlaceInfo PlaceInfo { get; init; }


    public CelestialMoment(DateTime dt, PlaceInfo placeInfo)
    {
        Moment = dt;
        SunInfo = new SunInfo(dt, placeInfo.Coordinates);
        MoonInfo = new MoonInfo(dt);
        PlaceInfo = placeInfo;
    }

    public CelestialMoment(DateTime dt, Coordinates coordinates, string placeName = "")
        : this(dt, new PlaceInfo(coordinates, placeName)) { }

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
