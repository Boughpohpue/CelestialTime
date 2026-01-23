using Infertus.Geo.Primitives;
using System.Text;

namespace CelestialTime;

public class PlaceInfo
{
    public string Name { get; set; }
    public Coordinates Coordinates { get; set; }


    public PlaceInfo(Coordinates coordinates, string name = "")
    {
        Name = name;
        Coordinates = coordinates;
    }

    public PlaceInfo(double latitude = 0.0, double longitude = 0.0, string name = "")
        : this(new Coordinates(latitude, longitude), name) { }


    public override string ToString()
    {
        var sb = new StringBuilder();
        if (!string.IsNullOrWhiteSpace(Name)) sb.AppendLine(Name);
        sb.AppendLine(Coordinates.ToDecimalString());
        sb.AppendLine(Coordinates.ToDmsString());
        return sb.ToString();
    }
}
