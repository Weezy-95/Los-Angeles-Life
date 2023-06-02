namespace Los_Angeles_Life.Factions;

public class FactionRank
{
    public static string FactionName { get; set; }
    public static string FactionRankName { get; set; }
    public static int FactionRankPermissions { get; set; }


    public FactionRank(string factionName, string factionRankName, int factionRankPermissions)
    {
        FactionName = factionName;
        FactionRankName = factionRankName;
        FactionRankPermissions = factionRankPermissions;
    }
}