using AltV.Net.Client;

namespace Los_Angeles_Life_Client.System;

public abstract class Marker : Client
{
    public static void Load()
    {
        Alt.OnServer("Client:Marker:Garage", () =>
        {
            Alt.Natives.DrawMarker(1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 231, 114, 0, 255, 
                false, false, 0, false, "Test", "Test", false);
        });
    }
}