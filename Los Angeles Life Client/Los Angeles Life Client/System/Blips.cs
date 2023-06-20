using AltV.Net.Client;
using AltV.Net.Data;

namespace Los_Angeles_Life_Client.System;

public abstract class Blips : Client
{
    public static void Load()
    {
        Alt.OnServer("Client:SendBlipList", (float positionX, float positionY, float positionZ, ushort blipId, byte blipClorId,
            string name) =>
        {
            CreateBlip(positionX, positionY, positionZ, blipId, blipClorId, name);
        });
    }

    private static void CreateBlip(float positionX, float positionY, float positionZ, ushort blipId, byte blipClorId,
        string name)
    {
        var blip = Alt.CreatePointBlip(new Position(positionX, positionY, positionZ));
        blip.Sprite = blipId;
        blip.Color = blipClorId;
        blip.Name = name;
        blip.Display = 3;
    }
}