using CelestialTime;


var places = new List<PlaceInfo>
{
    { new PlaceInfo(53.435719, 14.521007, "Szczecin, Poland") },
    { new PlaceInfo(52.234528, 20.991954, "Warszawa, Poland") },
    { new PlaceInfo(35.6768601, 139.7638947, "Tokyo, Japan") },
    { new PlaceInfo(45.4208777, -75.6901106, "Ottawa, Canada") },
    { new PlaceInfo(-45.8740984, 170.5035755, "Dunedin, New Zealand") },
};

var now = DateTime.Now;

foreach (var place in places)
{
    var cm = new CelestialMoment(now, place);
    Console.WriteLine(cm.ToString());
    Console.WriteLine();
}
