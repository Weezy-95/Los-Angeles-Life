using AltV.Net;
using AltV.Net.Data;
using Los_Angeles_Life.Entities;
using Los_Angeles_Life.Factions.StateFactions;
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

        public static bool CheckAccountExists(string discordId)
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

        public static int CreateAccount(string playerName, string discordId, ulong socialClub, long adminLevel, bool isWhitelisted, float cash, float bank,
            Position playerPosition, Rotation playerRotation, int playerDimension)
        {
            try
            {
                _connection.Open();

                MySqlCommand mySqlCommand = _connection.CreateCommand();
                mySqlCommand.CommandText =
                    "INSERT INTO accounts (DiscordId, PlayerName, SocialClub, AdminLevel, IsWhitelisted) " +
                    "VALUES (@discordId, @playerName, @socialClub, @adminLevel, @IsWhitelisted)";

                mySqlCommand.Parameters.AddWithValue("@discordId", discordId);
                mySqlCommand.Parameters.AddWithValue("@playerName", playerName);
                mySqlCommand.Parameters.AddWithValue("@socialClub", socialClub);
                mySqlCommand.Parameters.AddWithValue("@adminLevel", adminLevel);
                mySqlCommand.Parameters.AddWithValue("@isWhitelisted", isWhitelisted);

                mySqlCommand.ExecuteNonQuery();

                mySqlCommand.CommandText =
                    "INSERT INTO playerpositions (positionId, playerPosX, playerPosY, playerPosZ, playerRotation, playerDimension) " +
                    "VALUES (@positionId, @playerPosX, @playerPosY, @playerPosZ, @playerRotation, @playerDimension)";

                mySqlCommand.Parameters.AddWithValue("@positionId", discordId);
                mySqlCommand.Parameters.AddWithValue("@playerPosX", playerPosition.X);
                mySqlCommand.Parameters.AddWithValue("@playerPosY", playerPosition.Y);
                mySqlCommand.Parameters.AddWithValue("@playerPosZ", playerPosition.Z);
                mySqlCommand.Parameters.AddWithValue("@playerRotation", playerRotation.Yaw);
                mySqlCommand.Parameters.AddWithValue("@playerDimension", playerDimension);

                mySqlCommand.ExecuteNonQuery();

                mySqlCommand.CommandText =
                    "INSERT INTO playerfinances (playerFinanceId, cash, bank) " +
                    "VALUES (@playerFinanceId, @cash, @bank)";

                mySqlCommand.Parameters.AddWithValue("@playerFinanceId", discordId);
                mySqlCommand.Parameters.AddWithValue("@cash", cash);
                mySqlCommand.Parameters.AddWithValue("@bank", bank);

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
                        player.DiscordId = dataReader.GetString("DiscordID");
                        player.PlayerName = dataReader.GetString("PlayerName");
                        player.SocialClub = dataReader.GetUInt64("SocialClub");
                        player.AdminLevel = dataReader.GetInt16("AdminLevel");
                        player.IsWhitelisted = dataReader.GetBoolean("IsWhitelisted");
                    }
                }

                mySqlCommand.CommandText = 
                    "SELECT playerpositions.playerPosX, playerpositions.playerPosY, playerpositions.playerPosZ, playerpositions.playerRotation, playerpositions.playerDimension, accounts.discordId " +
                    "FROM playerpositions " +
                    "JOIN accounts " +
                    "ON playerpositions.positionId = accounts.discordId " +
                    "WHERE accounts.discordId=@discordId";

                using (MySqlDataReader dataReader = mySqlCommand.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        dataReader.Read();

                        Position loadedPosition = new Position(
                            dataReader.GetFloat("PlayerPosX"),
                            dataReader.GetFloat("PlayerPosY"), 
                            dataReader.GetFloat("PlayerPosZ"));

                        
                        Rotation loadedRotation = new Rotation(
                            0f, 
                            0f, 
                            dataReader.GetFloat("PlayerRotation"));
                        
                        player.PlayerDimension = dataReader.GetInt16("PlayerDimension");

                        player.PlayerPos = loadedPosition;
                        player.PlayerRot = loadedRotation;
                    }
                }

                mySqlCommand.CommandText =
                    "SELECT playerfinances.cash, playerfinances.bank, accounts.discordId " +
                    "FROM playerfinances " +
                    "JOIN accounts " +
                    "ON playerfinances.playerfinanceId = accounts.discordId " +
                    "WHERE accounts.discordId=@discordId";

                using (MySqlDataReader dataReader = mySqlCommand.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        dataReader.Read();

                        player.Cash = dataReader.GetFloat("Cash");
                        player.Bank = dataReader.GetFloat("Bank");
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
                mySqlCommand.CommandText = "UPDATE accounts SET playerName=@playerName, adminLevel=@adminLevel, isWhitelisted=@isWhitelisted WHERE discordId=@discordId";

                mySqlCommand.Parameters.AddWithValue("@discordId", player.DiscordId);
                mySqlCommand.Parameters.AddWithValue("@playerName", player.PlayerName);
                mySqlCommand.Parameters.AddWithValue("@adminLevel", player.AdminLevel);
                mySqlCommand.Parameters.AddWithValue("@isWhitelisted", player.IsWhitelisted);

                mySqlCommand.ExecuteNonQuery();

                mySqlCommand.CommandText =
                    "UPDATE playerfinances " +
                    "JOIN accounts " +
                    "ON playerfinances.playerfinanceId = accounts.discordId " +
                    "SET playerfinances.cash = @cash, playerfinances.bank = @bank " +
                    "WHERE accounts.discordId=@discordId";

                mySqlCommand.Parameters.AddWithValue("@cash", player.Cash);
                mySqlCommand.Parameters.AddWithValue("@bank", player.Bank);

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
        
        public static void SaveAllPlayersPositions(long discordId, Position playerPosition, Rotation playerRotation, int playerDimension)
        {
            try
            {
                _connection.Open();

                MySqlCommand mySqlCommand = _connection.CreateCommand();
                mySqlCommand.CommandText =
                    "UPDATE playerpositions " +
                    "JOIN accounts " +
                    "ON playerpositions.positionId = accounts.discordId " +
                    "SET playerpositions.playerPosX = @playerPosX, " +
                    "playerpositions.playerPosY = @playerPosY, " +
                    "playerpositions.playerPosZ = @playerPosZ, " +
                    "playerpositions.playerRotation = @playerRotation, " +
                    "playerpositions.playerDimension = @playerDimension " +
                    "WHERE accounts.discordId = @discordId";

                mySqlCommand.Parameters.AddWithValue("@playerPosX", playerPosition.X);
                mySqlCommand.Parameters.AddWithValue("@playerPosY", playerPosition.Y);
                mySqlCommand.Parameters.AddWithValue("@playerPosZ", playerPosition.Z);
                mySqlCommand.Parameters.AddWithValue("@playerRotation", playerRotation.Yaw);
                mySqlCommand.Parameters.AddWithValue("@playerDimension", playerDimension);
                mySqlCommand.Parameters.AddWithValue("@discordId", discordId);

                mySqlCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Alt.Log("[MySQL] Fehler beim Speichern der Position: " + ex);
                throw;
            }
            finally
            {
                _connection.Close();
            }
        }

        public static void SetAdminLevel(string discordId, int adminLevel)
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
                throw;
            }
            finally
            {
                _connection.Close();
            }
        }
    }
}