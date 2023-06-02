using AltV.Net;
using AltV.Net.Data;
using Los_Angeles_Life.Factions;
using Los_Angeles_Life.Factions.StateFactions;

using MySql.Data.MySqlClient;
using System.Data.Common;

namespace Los_Angeles_Life.Handlers
{
    abstract class FactionHandler
    {
        private static MySqlConnection connection;
        private static Dictionary<string, Faction> factionList;


        const string dbHost = "localhost";
        const string dbPort = "4406";
        const string dbUser = "dev";
        const string dbPassword = "Sonner2021$";
        const string dbName = "altv";

        const string connectionString = $"Server={dbHost};Port={dbPort};Database={dbName};Uid={dbUser};Pwd={dbPassword};";

        public static void LoadFactions()
        {
            connection = new MySqlConnection(connectionString);
            factionList = new Dictionary<string, Faction>();
            GetAllFactions();
        }

        public static void GetAllFactions()
        {
            try
            {
                connection.Open();

                MySqlCommand mySqlCommand = connection.CreateCommand();
                mySqlCommand.CommandText = "SELECT FactionId, FactionName, FactionLocationX, FactionLocationY, FactionLocationZ, FactionBlipId, FactionBlipColorId, FactionMoney FROM factions";
                MySqlDataReader reader = mySqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    string factionName = reader.GetString("FactionName");

                    Faction faction = new Faction();

                    if (factionList.ContainsKey(factionName))
                    {
                        faction = factionList[factionName];
                    }
                    else
                    {
                        int factionId = reader.GetInt16("FactionId");
                        Position factionLocation = new Position(
                            reader.GetFloat("FactionLocationX"),
                            reader.GetFloat("FactionLocationY"),
                            reader.GetFloat("FactionLocationZ"));
                        int factionBlipId = reader.GetInt16("FactionBlipId");
                        int factionBlipColorId = reader.GetInt16("FactionBlipColorId");
                        float factionMoney = reader.GetFloat("FactionMoney");

                        switch (factionName)
                        {
                            case "LSPD":
                                faction = new Lspd(factionId, factionName, factionLocation, factionBlipId, factionBlipColorId, factionMoney);
                                // LSPD Databaseeinträge abrufen und zuweisen
                                break;
                        }

                        factionList[factionName] = faction;
                    }
                }
            }
            catch (Exception ex)
            {
                Alt.Log("[MySQL] Fehler mit der Faction Abfrage: " + ex);
                throw;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}