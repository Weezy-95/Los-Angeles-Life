using AltV.Net.Client;

namespace Los_Angeles_Life_Client.System;

public abstract class Marker : Client
{
    public static void Load()
    {
        Alt.OnServer("Client:Marker:Garage", () =>
        {
            
        });
    }
}