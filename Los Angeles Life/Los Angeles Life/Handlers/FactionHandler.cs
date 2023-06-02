using AltV.Net;
using Los_Angeles_Life.Factions;
using Los_Angeles_Life.Factions.StateFactions;

namespace Los_Angeles_Life.Handlers
{
    abstract class FactionHandler
    {
        public static Dictionary<int, Faction> FactionDictionary { get; private set; }

        public static void LoadFactions()
        {
            if (FactionDictionary == null)
                FactionDictionary = new Dictionary<int, Faction>();
            LoadLspd();
        }

        private static void LoadLspd()
        {
            Faction lspd = DatabaseHandler.LoadLspd(new Lspd());


            FactionDictionary.Add(lspd.FactionId, lspd);

            Alt.Log("[Faction] Load LSPD" + lspd.FactionId + " ," + lspd.FactionName + " ," + lspd.FactionLocation + " ," + lspd.FactionBlipId + " ," + lspd.FactionBlipColorId + " ," + lspd.FactionMoney);
        }
    }
}