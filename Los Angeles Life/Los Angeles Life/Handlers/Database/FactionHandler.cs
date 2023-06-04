using AltV.Net;
using AltV.Net.Data;
using Los_Angeles_Life.Factions;
using Los_Angeles_Life.Factions.StateFactions;

using MySql.Data.MySqlClient;

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

        public static void LoadFactionsAndFactionRanks()
        {
            connection = new MySqlConnection(connectionString);
            factionList = new Dictionary<string, Faction>();
            GetAllFactions();
            GetAllFactionRanks();
        }

        public static bool GetAllFactions()
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
                                // LSPD Database Einträge abrufen und zuweisen
                                break;
                            case "Test1":
                                faction = new Test1(factionId, factionName, factionLocation, factionBlipId, factionBlipColorId, factionMoney);
                                // Test1 Database Einträge abrufen und zuweisen
                                break;
                            case "Test2":
                                faction = new Test2(factionId, factionName, factionLocation, factionBlipId, factionBlipColorId, factionMoney);
                                // Test2 Database Einträge abrufen und zuweisen
                                break;
                        }

                        factionList[factionName] = faction;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Alt.Log("[MySQL] Fehler mit der Faction Abfrage: " + ex);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool GetAllFactionRanks()
        {
            try
            {
                connection.Open();

                MySqlCommand mySqlCommand = connection.CreateCommand();
                mySqlCommand.CommandText = "SELECT FactionName, FactionRankName, FactionRankPermission FROM FactionRanks";
                MySqlDataReader reader = mySqlCommand.ExecuteReader();

                HashSet<string> existingRanks = new HashSet<string>();

                while (reader.Read())
                {
                    string factionName = reader.GetString("FactionName");
                    string factionRankName = reader.GetString("FactionRankName");
                    int factionRankPermission = reader.GetInt16("FactionRankPermission");

                    if (factionList.ContainsKey(factionName))
                    {
                        Faction faction = factionList[factionName];

                        string rankKey = $"{factionName}_{factionRankName}";

                        if (!existingRanks.Contains(rankKey))
                        {
                            FactionRank factionRank = new FactionRank(factionName, factionRankName, factionRankPermission);
                            faction.FactionRankList.Add(factionRank);

                            existingRanks.Add(rankKey);
                        }
                    }
                }

                reader.Close();
                connection.Close();

                return true;
            }
            catch(Exception ex)
            {
                Alt.Log("[MySQL] Fehler mit der FactionRanks Abfrage: " + ex);
                return false;
            }
            finally 
            { 
                connection.Close(); 
            }
        }

        public static bool RemoveFactionRank(string factionName, string factionRankName)
        {
            if (factionList.ContainsKey(factionName))
            {
                Faction faction = factionList[factionName];
                FactionRank rankToRemove = faction.FactionRankList.Find(rank => rank.FactionRankName == factionRankName);

                if (rankToRemove != null)
                {
                    try
                    {
                        faction.FactionRankList.Remove(rankToRemove);
                        connection.Open();

                        MySqlCommand mySqlCommand = connection.CreateCommand();
                        mySqlCommand.CommandText = "DELETE FROM FactionRanks WHERE FactionName = @FactionName AND FactionRankName = @FactionRankName";

                        mySqlCommand.Parameters.AddWithValue("@FactionName", factionName);
                        mySqlCommand.Parameters.AddWithValue("@FactionRankName", factionRankName);

                        MySqlDataReader reader = mySqlCommand.ExecuteReader();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        Alt.Log("[MySQL] Fehler beim Löschen des FactionRanks: " + ex);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
                return false;
            }
            return false;
        }

        public static bool RemoveAllFactionRanks(string factionName)
        {
            if (factionList.ContainsKey(factionName))
            {
                Faction faction = factionList[factionName];
                faction.FactionRankList.Clear();

                try
                {
                    connection.Open();

                    MySqlCommand mySqlCommand = connection.CreateCommand();
                    mySqlCommand.CommandText = "DELETE FROM FactionRanks WHERE FactionName = @FactionName";
                    mySqlCommand.Parameters.AddWithValue("@FactionName", factionName);
                    MySqlDataReader reader = mySqlCommand.ExecuteReader();

                    if (reader.RecordsAffected == 0)
                    {
                        return false;
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    Alt.Log("[MySQL] Fehler beim Löschen der FactionRanks: " + ex);
                }
                finally
                {
                    connection.Close();
                }

                return false;
            }
            return false;
        }

        public static bool AddFactionRank(string factionName, string factionRankName, int factionRankPermission)
        {
            if (factionList.ContainsKey(factionName))
            {
                Faction faction = factionList[factionName];

                FactionRank newRank = new FactionRank(factionName, factionRankName, factionRankPermission);
                faction.FactionRankList.Add(newRank);

                try
                {
                    connection.Open();

                    MySqlCommand mySqlCommand = connection.CreateCommand();
                    mySqlCommand.CommandText = "INSERT INTO FactionRanks (FactionName, FactionRankName, FactionRankPermission) VALUES (@FactionName, @FactionRankName, @FactionRankPermission)";
                    mySqlCommand.Parameters.AddWithValue("@FactionName", factionName);
                    mySqlCommand.Parameters.AddWithValue("@FactionRankName", factionRankName);
                    mySqlCommand.Parameters.AddWithValue("@FactionRankPermission", factionRankPermission);
                    mySqlCommand.ExecuteNonQuery();

                    return true;
                }
                catch (Exception ex)
                {
                    Alt.Log("[MySQL] Fehler beim Hinzufügen des FactionRanks: " + ex);
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
            return false;
        }

        public static bool UpdateFactionRankPermission(string factionName, string factionRankName, int newFactionRankPermission)
        {
            if (factionList.ContainsKey(factionName))
            {
                Faction faction = factionList[factionName];
                FactionRank factionRank = faction.FactionRankList.Find(rank => rank.FactionRankName == factionRankName);

                if (factionRank != null)
                {
                    factionRank.FactionRankPermissions = newFactionRankPermission;

                    try
                    {
                        connection.Open();

                        MySqlCommand mySqlCommand = connection.CreateCommand();
                        mySqlCommand.CommandText = "UPDATE FactionRanks SET FactionRankPermission = @NewFactionRankPermission WHERE FactionName = @FactionName AND FactionRankName = @FactionRankName";

                        mySqlCommand.Parameters.AddWithValue("@NewFactionRankPermission", newFactionRankPermission);
                        mySqlCommand.Parameters.AddWithValue("@FactionName", factionName);
                        mySqlCommand.Parameters.AddWithValue("@FactionRankName", factionRankName);

                        mySqlCommand.ExecuteNonQuery();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        Alt.Log("[MySQL] Fehler beim Aktualisieren der FactionRankPermission: " + ex);
                        throw;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
                return false;
            }
            return false;
        }

        public static bool UpdateFactionRankName(string factionName, string oldFactionRankName, string newFactionName)
        {
            if (factionList.ContainsKey(factionName))
            {
                Faction faction = factionList[factionName];
                FactionRank rankToUpdate = faction.FactionRankList.Find(rank => rank.FactionRankName == oldFactionRankName);

                if (rankToUpdate != null)
                {
                    try
                    {
                        faction.FactionName = newFactionName;
                        connection.Open();

                        MySqlCommand updateFactionNameCommand = connection.CreateCommand();
                        updateFactionNameCommand.CommandText = "UPDATE FactionRanks SET FactionRankName = @NewFactionName WHERE FactionName = @FactionName AND FactionRankName = @OldFactionRankName";
                        updateFactionNameCommand.Parameters.AddWithValue("@NewFactionName", newFactionName);
                        updateFactionNameCommand.Parameters.AddWithValue("@FactionName", factionName);
                        updateFactionNameCommand.Parameters.AddWithValue("@OldFactionRankName", oldFactionRankName);
                        updateFactionNameCommand.ExecuteNonQuery();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        Alt.Log("[MySQL] Fehler beim Aktualisieren des FactionNames: " + ex);
                        throw;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
                return false;
            }
            return false;
        }
    }
}