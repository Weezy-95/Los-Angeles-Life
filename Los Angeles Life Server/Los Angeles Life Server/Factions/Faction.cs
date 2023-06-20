using AltV.Net.Data;

namespace Los_Angeles_Life_Server.Factions;

public class Faction
{
    public int FactionId { get; set; }
    public string FactionName { get; set; }
    public Position FactionLocation { get; set; }
    public int FactionBlipId { get; set; }
    public int FactionBlipColorId { get; set; }
    public float FactionMoney { get; set; }
    public List<FactionRank> FactionRankList { get; set; }

    public Faction()
    {
        FactionRankList = new List<FactionRank>();
    }
}