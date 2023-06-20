using AltV.Net.Client;

namespace Los_Angeles_Life_Client.System;

public abstract class Time : Client
{
    public static void Load()
    {
        Alt.OnConnectionComplete += () =>
        {
            Alt.MsPerGameMinute = 60000;
        };
    }
}