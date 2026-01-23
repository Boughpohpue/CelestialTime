# CelestialTime

**CelestialTime** is a lightweight C# library for deterministic solar and lunar calculations.  
It allows developers to calculate sun events, moon phases, illumination, and approximate eclipse timings based on a `DateTime` and a geographic location — all through a clean and expressive API.


## Features

- **Sun events**
  - Sunrise & sunset times
  - Day and night lengths
  - Solar eclipse detection and forecasting
  - Time since last or until next sunrise/sunset/eclipse
  
- **Moon phases**
  - Current phase, percent illumination, and age
  - Last and next new/full moon
  - Lunar eclipse detection and forecasting
  - Phase-based calculations (Waxing Crescent, Full Moon, etc.)

- **Time zone handling**
  - Automatically calculates local time based on latitude/longitude

- **Immutable, easy-to-use models**
  - `SolInfo` - sun-centric info for a specific location and date
  - `LunaInfo` - moon-centric info for a specific date
  - `CelestialMoment` - aggregates sun, moon, and location data
  - `PlaceInfo` - stores geographic coordinates and optional name

---

## Installation

Install via NuGet:

```bash
dotnet add package Infertus.CelestialTime
```

or via the Package Manager Console:

```bash
Install-Package Infertus.CelestialTime
```


## Usage

** Basic example

using CelestialTime;
using System;


var place = new PlaceInfo(45.42, -75.69, "Ottawa, Canada");
var moment = new CelestialMoment(DateTime.Now, place);

Console.WriteLine(moment.SunInfo.ToString());
Console.WriteLine(moment.MoonInfo.ToString());


** Sample output:

SUN INFO:
Local time: 1/22/2026 12:16 PM
Day length: 9h 21m
Sunrise: 7:34 AM
Sunset: 4:55 PM
Next eclipse: 2/17/2026, in 25d

MOON INFO:
Phase: Waxing Crescent
Age: 4d 9h 38m
Illumination: 14.91%
Next full moon: 2/2/2026, in 10d 8h


** Multiple locations

var places = new List<PlaceInfo>
{
    new PlaceInfo(53.435719, 14.521007, "Szczecin, Poland"),
    new PlaceInfo(35.6768601, 139.7638947, "Tokyo, Japan")
};

foreach (var p in places)
{
    var cm = new CelestialMoment(now, p);
    Console.WriteLine(cm.ToString());
}


This will output sun and moon data for each location, including rise/set times, moon phase, and eclipse information.



# API Overview

** DateTime extension methods

GetSunrise(latitude, longitude)

GetSunset(latitude, longitude)

IsSolarEclipse(), GetNextSolarEclipse(), GetLastSolarEclipse()

IsLunarEclipse(), GetMoonPhase(), GetMoonAgePercent(), GetMoonIllumination(), etc.


** Data models

PlaceInfo – represents a location

SunInfo – sun-centric calculations

MoonInfo – moon-centric calculations

CelestialMoment – aggregates sun + moon + location


All models provide ToString() overrides for human-readable output.



## License

This library is licensed under GPL-3.0-or-later



## Notes

Accurate to a reasonable approximation — ideal for astronomy apps, planners, or educational tools.

Supports any location on Earth via latitude and longitude.

Time zone conversions are automatic; no extra configuration needed.
