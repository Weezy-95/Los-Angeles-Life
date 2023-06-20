using AltV.Net.Client;

namespace Los_Angeles_Life_Client.System
{
    public abstract class Weather : Client
    {
        public static void Load()
        {
            Alt.OnConnectionComplete += () =>
            {
                string? value = null;

                if (value != null) ChangeWeather("ChangeWeather", value);
            };

            Alt.OnGlobalSyncedMetaChange += OnGlobalSyncedMetaChange;
        }

        private static void ChangeWeather(string key, object value)
        {
            if (key != "ChangeWeather") return;

            Alt.Natives.SetWeatherTypeOvertimePersist(value.ToString(), 5);
        }

        private static void OnGlobalSyncedMetaChange(string key, object value, object oldValue)
        {
            ChangeWeather(key, value);
        }
    }
}