using AltV.Net;
using AltV.Net.Data;
using Los_Angeles_Life_Server.Factions;
using Los_Angeles_Life_Server.Factions.StateFactions;
using MySql.Data.MySqlClient;

namespace Los_Angeles_Life_Server.Handlers
{
    abstract class FactionHandler
    {
        public static Dictionary<string, Faction> factionList;

        public static void LoadFactionsAndFactionRanks()
        {
            factionList = new Dictionary<string, Faction>();
            GetAllFactions();
            GetAllFactionRanks();
        }

        public static bool GetAllFactions()
        {
            try
            {
                MySqlConnection connection = DatabaseHandler.OpenConnection();

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
                                break;
                            case "LSMC":
                                faction = new Lsmc(factionId, factionName, factionLocation, factionBlipId, factionBlipColorId, factionMoney);
                                break;
                            case "FIB":
                                faction = new Fib(factionId, factionName, factionLocation, factionBlipId, factionBlipColorId, factionMoney);
                                break;
                            case "ACLS":
                                faction = new Acls(factionId, factionName, factionLocation, factionBlipId, factionBlipColorId, factionMoney);
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
                DatabaseHandler.CloseConnection();
            }
        }

        public static bool GetAllFactionRanks()
        {
            try
            {
                MySqlConnection connection = DatabaseHandler.OpenConnection();

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
                DatabaseHandler.CloseConnection();
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
                        MySqlConnection connection = DatabaseHandler.OpenConnection();

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
                        DatabaseHandler.CloseConnection();
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
                    MySqlConnection connection = DatabaseHandler.OpenConnection();

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
                    DatabaseHandler.CloseConnection();
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
                    MySqlConnection connection = DatabaseHandler.OpenConnection();

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
                    DatabaseHandler.CloseConnection();
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
                        MySqlConnection connection = DatabaseHandler.OpenConnection();

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
                        DatabaseHandler.CloseConnection();
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
                        MySqlConnection connection = DatabaseHandler.OpenConnection();

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
                        DatabaseHandler.CloseConnection();
                    }
                }
                return false;
            }
            return false;
        }
    }
}