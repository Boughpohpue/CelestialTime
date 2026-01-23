using CelestialTime;


Console.WriteLine($"{new MoonInfo(new DateTime(2026, 2, 2))}\n");
Console.WriteLine($"{new MoonInfo(DateTime.Now.AddYears(-6))}\n");
Console.WriteLine($"{new SunInfo(DateTime.Now.AddDays(-963))}\n");
Console.WriteLine($"On {DateTime.Now.AddDays(144):d} moon in {DateTime.Now.AddDays(144).GetMoonPhase()} phase\n\n");
Console.WriteLine($"On {DateTime.Now.AddMonths(3):d} sunrise at {DateTime.Now.AddMonths(3).GetSunriseTime(53.435719, 14.521007):t}\n\n");

Console.WriteLine(string.Join("\n\n", new[]
{
    new PlaceInfo(53.435719,   14.521007,   "Szczecin, Poland"),
    new PlaceInfo(52.234528,   20.991954,   "Warszawa, Poland"),
    new PlaceInfo(35.6768601,  139.7638947, "Tokyo, Japan"),
    new PlaceInfo(45.4208777,  -75.6901106, "Ottawa, Canada"),
    new PlaceInfo(-45.8740984, 170.5035755, "Dunedin, New Zealand")
}.Select(p => $"{new CelestialMoment(DateTime.Now, p)}")));
