using AltV.Net;
using AltV.Net.Data;
using Los_Angeles_Life.Entities;
using MySql.Data.MySqlClient;

namespace Los_Angeles_Life.Handlers
{
    public class DatabaseHandler
    {
        private static MySqlConnection _connection;

        public DatabaseHandler()
        {
            const string dbHost = "localhost";
            const string dbPort = "4406";
            const string dbUser = "dev";
            const string dbPassword = "Sonner2021$";
            const string dbName = "altv";

            const string connectionString = $"Server={dbHost};Port={dbPort};Database={dbName};Uid={dbUser};Pwd={dbPassword};";

            _connection = new MySqlConnection(connectionString);
        }

        public void OpenConnection()
        {
            try
            {
                _connection.Open();
                Alt.Log("[MySQL] Datenbank Verbindung erfolgreich aufgebaut!");
            }
            catch (MySqlException ex)
            {
                Alt.Log("[MySQL] Problem mit der Datenbank Verbindung! Grund: " + ex);
                throw;
            }
        }

        public void CloseConnection()
        {
            _connection.Close();
        }

        public void LoadPlayerCount()
        {
            try
            {
                _connection.Open();

                var mySqlCommand = _connection.CreateCommand();
                mySqlCommand.CommandText = "SELECT * FROM accounts";

                var count = 0;
                using (MySqlDataReader reader = mySqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var id = reader.GetInt32("PlayerID");
                        count++;
                    }
                }

                Alt.Log($"[MySQL] Es wurden erfolgreich {count} Accounts geladen!");
            }
            catch (MySqlException ex)
            {
                Alt.Log("[MySQL] Fehler beim Laden der Spieler! Grund: " + ex);
                throw;
            }
            finally
            {
                _connection.Close();
            }
        }

        public static bool CheckAccountExists(long discordId)
        {
            try
            {
                _connection.Open();
                
                MySqlCommand mySqlCommand = _connection.CreateCommand();
                mySqlCommand.CommandText = "SELECT * FROM accounts WHERE discordId=@discordId";
                mySqlCommand.Parameters.AddWithValue("@discordId", discordId);

                using MySqlDataReader dataReader = mySqlCommand.ExecuteReader();
                if (dataReader.HasRows)
                {
                    return true;
                }

                return false;
            }
            catch (MySqlException ex)
            {
                Alt.Log("[MySQL] Fehler mit der Account Abfrage: " + ex);
                throw;
            }
            finally
            {
                _connection.Close();
            }
        }

        public static int CreateAccount(string playerName, long discordId, ulong socialClub, long adminLevel, long money, bool isWhitelisted, 
            Position playerPosition, Rotation playerRotation, int playerDim)
        {
            try
            {
                _connection.Open();

                MySqlCommand mySqlCommand = _connection.CreateCommand();
                mySqlCommand.CommandText =
                    "INSERT INTO accounts (DiscordId, PlayerName, SocialClub, AdminLevel, Money, IsWhitelisted, PlayerPosX, PlayerPosY, PlayerPosZ, PlayerRot, PlayerDim) " +
                    "VALUES (@discordId, @playerName, @socialClub, @adminLevel, @Money, @IsWhitelisted, @playerPosX, @playerPosY, @playerPosZ, @playerRot, @playerDim)";

                mySqlCommand.Parameters.AddWithValue("@discordId", discordId);
                mySqlCommand.Parameters.AddWithValue("@playerName", playerName);
                mySqlCommand.Parameters.AddWithValue("@socialClub", socialClub);
                mySqlCommand.Parameters.AddWithValue("@adminLevel", adminLevel);
                mySqlCommand.Parameters.AddWithValue("@money", money);
                mySqlCommand.Parameters.AddWithValue("@isWhitelisted", isWhitelisted);
                mySqlCommand.Parameters.AddWithValue("@playerPosX", playerPosition.X);
                mySqlCommand.Parameters.AddWithValue("@playerPosY", playerPosition.Y);
                mySqlCommand.Parameters.AddWithValue("@playerPosZ", playerPosition.Z);
                mySqlCommand.Parameters.AddWithValue("@playerRot", playerRotation.Yaw);
                mySqlCommand.Parameters.AddWithValue("@playerDim", playerDim);

                mySqlCommand.ExecuteNonQuery();

                return (int)mySqlCommand.LastInsertedId;
            }
            catch (MySqlException ex)
            {
                Alt.Log("[MySQL] Account wurde nicht erstellt! Grund: " + ex);
                return -1;
            }
            finally
            {
                _connection.Close();
            }
        }


        public static void LoadAccount(MyPlayer player)
        {
            try
            {
                _connection.Open();

                MySqlCommand mySqlCommand = _connection.CreateCommand();
                mySqlCommand.CommandText = "SELECT * FROM accounts WHERE discordId=@discordId";

                mySqlCommand.Parameters.AddWithValue("@discordId", player.DiscordId);

                using (MySqlDataReader dataReader = mySqlCommand.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        dataReader.Read();
                        player.PlayerId = dataReader.GetInt32("PlayerID");
                        player.DiscordId = dataReader.GetInt64("DiscordID");
                        player.PlayerName = dataReader.GetString("PlayerName");
                        player.SocialClub = dataReader.GetUInt64("SocialClub");
                        player.Money = dataReader.GetInt32("Money");
                        player.AdminLevel = dataReader.GetInt16("AdminLevel");
                        player.IsWhitelisted = dataReader.GetBoolean("IsWhitelisted");
                        Position loadedPosition = new Position(dataReader.GetFloat("PlayerPosX"),
                            dataReader.GetFloat("PlayerPosY"), dataReader.GetFloat("PlayerPosZ"));
                        player.PlayerPos = loadedPosition;
                        Rotation loadedRotation = new Rotation(0f, 0f, dataReader.GetFloat("PlayerRot"));
                        player.PlayerRot = loadedRotation;
                        player.PlayerDim = dataReader.GetInt16("PlayerDim");
                        Alt.Log("DB: " + player.PlayerDim);
                    }
                }
            }
            catch (MySqlException ex)
            {
                Alt.Log("[MySQL] Fehler beim Laden des Accounts: " + ex);
                throw;
            }
            finally
            {
                _connection.Close();
            }
        }
        
        public static void SaveAccount(MyPlayer player)
        {
            try
            {
                _connection.Open();

                MySqlCommand mySqlCommand = _connection.CreateCommand();
                mySqlCommand.CommandText =
                    "UPDATE accounts SET playerName=@playerName, money=@money, adminLevel=@adminLevel, isWhitelisted=@isWhitelisted, " +
                    "playerDim=@playerDim WHERE discordId=@discordId";

                mySqlCommand.Parameters.AddWithValue("@discordId", player.DiscordId);
                mySqlCommand.Parameters.AddWithValue("@playerName", player.PlayerName);
                mySqlCommand.Parameters.AddWithValue("@money", player.Money);
                mySqlCommand.Parameters.AddWithValue("@adminLevel", player.AdminLevel);
                mySqlCommand.Parameters.AddWithValue("@isWhitelisted", player.IsWhitelisted);
                //mySqlCommand.Parameters.AddWithValue("@playerPosX", player.PlayerPos.X);
                //mySqlCommand.Parameters.AddWithValue("@playerPosY", player.PlayerPos.Y);
                //mySqlCommand.Parameters.AddWithValue("@playerPosZ", player.PlayerPos.Z);
                //mySqlCommand.Parameters.AddWithValue("@playerRot", player.Rotation.Yaw);
                mySqlCommand.Parameters.AddWithValue("@playerDim", player.PlayerDim);

                mySqlCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Alt.Log("[MySQL] Fehler beim speichern des Accounts: " + ex);
                throw;
            }
            finally
            {
                _connection.Close();
            }
        }
        
        public static void SaveAllPlayersPositions(long discordId, Position playerPosition, Rotation playerRotation)
        {
            try
            {
                _connection.Open();

                MySqlCommand mySqlCommand = _connection.CreateCommand();
                mySqlCommand.CommandText =
                    "UPDATE accounts SET playerPosX=@playerPosX, playerPosY=@playerPosY, playerPosZ=@playerPosZ, playerRot=@playerRot WHERE discordId=@discordId";

                mySqlCommand.Parameters.AddWithValue("@playerPosX", playerPosition.X);
                mySqlCommand.Parameters.AddWithValue("@playerPosY", playerPosition.Y);
                mySqlCommand.Parameters.AddWithValue("@playerPosZ", playerPosition.Z);
                mySqlCommand.Parameters.AddWithValue("@playerRot", playerRotation.Yaw);
                mySqlCommand.Parameters.AddWithValue("@discordId", discordId);

                mySqlCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Alt.Log("[MySQL] Fehler beim Speichern der Position: " + ex);
            }
            finally
            {
                _connection.Close();
            }
        }

        public static void SetAdminLevel(long discordId, int adminLevel)
        {
            try
            {
                _connection.Open();

                MySqlCommand mySqlCommand = _connection.CreateCommand();
                mySqlCommand.CommandText =
                    "UPDATE accounts SET adminLevel=@adminLevel WHERE discordId=@discordId";

                mySqlCommand.Parameters.AddWithValue("@adminLevel", adminLevel);
                mySqlCommand.Parameters.AddWithValue("@discordId", discordId);

                mySqlCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Alt.Log("[MySQL] Fehler setzen des AdminLevels: " + ex);
            }
            finally
            {
                _connection.Close();
            }
        }
    }
}