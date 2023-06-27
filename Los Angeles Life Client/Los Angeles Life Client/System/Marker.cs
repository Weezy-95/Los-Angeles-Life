using AltV.Net.Client;
using AltV.Net.Data;

namespace Los_Angeles_Life_Client.System;

public abstract class Marker : Client
{
    public static void Load()
    {
        /*
        Alt.OnServer("Client:Marker:Garage", (List<Position> positions) =>
        {
            CreateMarker(positions);
        });
        */
    }

    private static void CreateMarker(List<Position> positions)
    {
        foreach(Position position in positions)
        {
            Alt.Log("Marker ertellt: " + position.ToString());
        }
    }
}