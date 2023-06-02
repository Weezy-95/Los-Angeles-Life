
using AltV.Net.Data;

namespace Los_Angeles_Life.Factions.StateFactions;

public class Lspd : Faction
{
    public Lspd(int factionId, string factionName, Position factionLocation, int factionBlipId, int factionBlipColorId, float factionMoney)
    {
        FactionId = factionId;
        FactionLocation = factionLocation;
        FactionBlipId = factionBlipId;
        FactionBlipColorId = factionBlipColorId;
        FactionMoney = factionMoney;
    }
}