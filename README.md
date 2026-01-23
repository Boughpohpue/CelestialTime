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

**Basic example**

```C#
using CelestialTime;
using System;


var place = new PlaceInfo(52.234528, 20.991954, "Warszawa, Poland");
var moment = new CelestialMoment(DateTime.Now, place);

Console.WriteLine(moment.SunInfo.ToString());
Console.WriteLine(moment.MoonInfo.ToString());
```

**Sample output:**

```bash
SUN INFO:
Info time:      1/23/2026 6:08:26 AM
Local time:     1/23/2026 6:08:26 AM
Day of year:    23
Day length:     8h 35m
Night length:   15h 24m
Sunrise time:   7:30 AM
Sunset time:    4:06 PM
Last sunrise:   22h 36m ago
Next sunrise:   in 1h 21m
Last sunset:    14h 3m ago
Next sunset:    in 9h 57m
Last eclipse:   9/21/2025, 124d ago
Next eclipse:   2/17/2026, in 24d

MOON INFO:
Info time:      1/23/2026 6:08:26 AM
Age:            4d 21h 30m (16.58%)
Phase:          Waxing crescent
Illumination:   33.16%
Last new moon:  1/18/2026 8:38:14 AM, 4d 21h 30m ago
Next new moon:  2/16/2026 9:22:17 PM, in 24d 15h 13m
Last full moon: 1/3/2026 2:16:12 PM, 19d 15h 52m ago
Next full moon: 2/2/2026 3:00:15 AM, in 9d 20h 51m
Last eclipse:   9/7/2025, 138d 6h 8m ago
Next eclipse:   3/3/2026, in 38d 17h 51m
```

**Multiple locations**

```C#
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
```

This will output sun and moon data for each location, including rise/set times, moon phase, and eclipse information.



# API Overview

**DateTime extension methods**

GetSunrise(latitude, longitude)

GetSunset(latitude, longitude)

IsSolarEclipse(), GetNextSolarEclipse(), GetLastSolarEclipse()

IsLunarEclipse(), GetMoonPhase(), GetMoonAgePercent(), GetMoonIllumination(), etc.


**Data models**

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
