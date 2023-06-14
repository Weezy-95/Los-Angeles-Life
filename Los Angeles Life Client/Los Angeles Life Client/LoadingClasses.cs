using Los_Angeles_Life_Client.System;
using Los_Angeles_Life_Client.World;

namespace Los_Angeles_Life_Client;

public static class LoadingClasses
{
    public static void LoadClasses()
    {
        MissedInterriors.Load();
        Sound.Load();
        DiscordAuth.Load();
        Time.Load();
        Weather.Load();
        Ped.Load();
        Notification.Load();
        Marker.Load();
        Garage.Load();
        Blips.Load();
        Hud.Load();
    }
}