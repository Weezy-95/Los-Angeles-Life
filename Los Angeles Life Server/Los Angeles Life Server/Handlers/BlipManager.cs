﻿using AltV.Net;
using Los_Angeles_Life_Server.Entities;
using Los_Angeles_Life_Server.Garages;
using Los_Angeles_Life_Server.Handlers.Database;

namespace Los_Angeles_Life_Server.Handlers;

public abstract class BlipManager : IScript
{
    public static void CreateFactionBlips(MyPlayer player)
    {
        foreach (var faction in FactionHandler.factionList.Values)
        {
            var locX = faction.FactionLocation.X;
            var locY = faction.FactionLocation.Y;
            var locZ = faction.FactionLocation.Z;
            var blip = faction.FactionBlipId;
            var blipColor = faction.FactionBlipColorId;
            var name = faction.FactionName;
            
            player.Emit("Client:SendBlipList", locX, locY, locZ, blip, blipColor, name);
        }
    }

    public static void CreateGarageBlips(MyPlayer player)
    {
        foreach(KeyValuePair<int, Garage> garageEntry in GarageHandler.garageList)
        {
            player.Emit("Client:SendBlipList",
                garageEntry.Value.Location.X,
                garageEntry.Value.Location.Y,
                garageEntry.Value.Location.Z,
                garageEntry.Value.BlipId,
                garageEntry.Value.BlipColorId,
                garageEntry.Value.Name);
        }
    }
}