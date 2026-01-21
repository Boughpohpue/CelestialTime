using System.Text;

namespace CelestialTime;

public class PlaceInfo
{
    public string Name { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public PlaceInfo(double latitude = 0.0, double longitude = 0.0, string name = "")
    {
        Name = name;
        Latitude = latitude; 
        Longitude = longitude; 
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        if (!string.IsNullOrWhiteSpace(Name)) sb.AppendLine(Name);
        sb.AppendLine($"{Latitude}, {Longitude}");
        sb.AppendLine($"{Angle.DegToDms(Latitude, 'N', 'S')}, {Angle.DegToDms(Longitude, 'E', 'W')}");
        return sb.ToString();
    }
}
