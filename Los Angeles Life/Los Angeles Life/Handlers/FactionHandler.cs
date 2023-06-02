using AltV.Net;
using Los_Angeles_Life.Factions;
using Los_Angeles_Life.Factions.StateFactions;

namespace Los_Angeles_Life.Handlers
{
    abstract class FactionHandler
    {
        public static Dictionary<int, Faction> factionList = new Dictionary<int, Faction>();

        public static void LoadFactions()
        {
            LoadLspd();
        }

        private static void LoadLspd()
        {
            Faction lspd = DatabaseHandler.selectLspdFromDatabase(new Lspd("LSPD"));

            factionList.Add(lspd.FactionId, lspd);
        }
    }
}