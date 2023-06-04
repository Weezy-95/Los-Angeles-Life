
using AltV.Net.Data;

namespace Los_Angeles_Life.Factions.StateFactions;

public class Test1 : Faction
{
    public Test1(int factionId, string factionName, Position factionLocation, int factionBlipId, int factionBlipColorId, float factionMoney)
    {
        FactionId = factionId;
        FactionName = factionName;
        FactionLocation = factionLocation;
        FactionBlipId = factionBlipId;
        FactionBlipColorId = factionBlipColorId;
        FactionMoney = factionMoney;
    }
}