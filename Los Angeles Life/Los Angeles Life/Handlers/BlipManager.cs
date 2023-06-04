using AltV.Net;
using Los_Angeles_Life.Entities;

namespace Los_Angeles_Life.Handlers;

public abstract class BlipManager : IScript
{
    public static void CreateBlip(MyPlayer player)
    {
        foreach (var faction in FactionHandler.factionList.Values)
        {
            var locX = faction.FactionLocation.X;
            var locY = faction.FactionLocation.Y;
            var locZ = faction.FactionLocation.Z;
            var blip = faction.FactionBlipId;
            var blipColor = faction.FactionBlipColorId;
            var name = faction.FactionName;
            
            player.Emit("Client:SendFactionList", locX, locY, locZ, blip, blipColor, name);
        }
    }
}