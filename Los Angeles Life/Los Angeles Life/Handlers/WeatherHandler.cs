using AltV.Net;
using AltV.Net.Enums;

namespace Los_Angeles_Life.Handlers
{
    public abstract class WeatherHandler
    {
        private static Dictionary<int, WeatherType[]> weatherTypes = new();
        private static Timer? weatherTimer;
        private const double weatherPatternTime = 80;
        private static WeatherType[] currentWeatherPattern;

        private static WeatherType[] rainyWeather = {
            WeatherType.Clearing,
            WeatherType.Rain,
            WeatherType.Thunder,
            WeatherType.Foggy
        };

        private static WeatherType[] snowyWeather = {
            WeatherType.Snowlight,
            WeatherType.Snow,
            WeatherType.Blizzard,
            WeatherType.Snowlight
        };

        public static void StartWeather()
        {
            weatherTypes.Add(1, rainyWeather);
            weatherTypes.Add(2, snowyWeather);
            new Timer(StartWeatherPattern, null, TimeSpan.Zero, TimeSpan.FromSeconds(weatherPatternTime));
        }

        private static void StartWeatherPattern(object? state)
        {
            currentWeatherPattern = GetRandomWeatherState();
            weatherTimer = new Timer(ChangeWeatherInPattern, null, TimeSpan.Zero, TimeSpan.FromSeconds(weatherPatternTime / currentWeatherPattern.Length));
        }

        private static void ChangeWeatherInPattern(object? state)
        {
            if (currentWeatherPattern.Length > 0)
            {
                WeatherType nextWeather = currentWeatherPattern[0];
 
                Alt.SetSyncedMetaData("ChangeWeather", nextWeather.ToString().ToUpper());
                currentWeatherPattern = currentWeatherPattern[1..];
                Alt.Log(nextWeather.ToString().ToUpper());

                if (currentWeatherPattern.Length == 0)
                {
                    StopChangeInPatternTimer();
                }
            } 
        }

        private static void StopChangeInPatternTimer()
        {
            if (weatherTimer != null)
            {
                weatherTimer.Dispose();
                weatherTimer = null;
            }
        }

        private static WeatherType[] GetRandomWeatherState()
        {
            Random random = new Random();
            int weatherIndex = random.Next(1, weatherTypes.Count + 1);

            WeatherType[] weatherPattern = weatherTypes[weatherIndex];
            Alt.Log("GetRandomWeatherState: " + weatherIndex);

            return weatherPattern;
        }
    }
}
