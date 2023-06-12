namespace Los_Angeles_Life_Server.Factions;

public class FactionRank
{
    public string FactionName { get; set; }
    public string FactionRankName { get; set; }
    public int FactionRankPermissions { get; set; }


    public FactionRank(string factionName, string factionRankName, int factionRankPermissions)
    {
        FactionName = factionName;
        FactionRankName = factionRankName;
        FactionRankPermissions = factionRankPermissions;
    }
}