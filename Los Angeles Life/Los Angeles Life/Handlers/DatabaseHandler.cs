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

        public void LoadAllPlayers()
        {
            try
            {
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
        }

        public static bool CheckAccountExists(long discordId)
        {
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

        public static int CreateAccount(string playerName, long discordId, ulong socialClub, long adminLevel, long money, bool isWhitelisted, float playerPosX, float playerPosY, float playerPosZ)
        {
            try
            {
                MySqlCommand mySqlCommand = _connection.CreateCommand();
                mySqlCommand.CommandText = "INSERT INTO accounts (DiscordId, PlayerName, SocialClub, AdminLevel, Money, IsWhitelisted, PlayerPosX, PlayerPosY, PlayerPosZ) " +
                                           "VALUES (@discordId, @playerName, @socialClub, @adminLevel, @Money, @IsWhitelisted, @playerPosX, @playerPosY, @playerPosZ)";

                mySqlCommand.Parameters.AddWithValue("@discordId", discordId);
                mySqlCommand.Parameters.AddWithValue("@playerName", playerName);
                mySqlCommand.Parameters.AddWithValue("@socialClub", socialClub);
                mySqlCommand.Parameters.AddWithValue("@adminLevel", adminLevel);
                mySqlCommand.Parameters.AddWithValue("@money", money);
                mySqlCommand.Parameters.AddWithValue("@isWhitelisted", isWhitelisted);
                mySqlCommand.Parameters.AddWithValue("@playerPosX", playerPosX);
                mySqlCommand.Parameters.AddWithValue("@playerPosY", playerPosY);
                mySqlCommand.Parameters.AddWithValue("@playerPosZ", playerPosZ);


                mySqlCommand.ExecuteNonQuery();

                return (int)mySqlCommand.LastInsertedId;
            }
            catch (MySqlException ex)
            {
                Alt.Log("[MySQL] Account wurde nicht erstellt! Grund: " + ex);
                return -1;
            }
        }


        public static void LoadAccount(MyPlayer myPlayer)
        {
            MySqlCommand mySqlCommand = _connection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM accounts WHERE discordId=@discordId";

            mySqlCommand.Parameters.AddWithValue("@discordId", myPlayer.DiscordId);

            using (MySqlDataReader dataReader = mySqlCommand.ExecuteReader())
            {
                if (dataReader.HasRows)
                {
                    dataReader.Read();
                    myPlayer.PlayerId = dataReader.GetInt32("PlayerID");
                    myPlayer.DiscordId = dataReader.GetInt64("DiscordID");
                    myPlayer.PlayerName = dataReader.GetString("PlayerName");
                    myPlayer.SocialClub = dataReader.GetUInt64("SocialClub");
                    myPlayer.Money = dataReader.GetInt32("Money");
                    myPlayer.AdminLevel = dataReader.GetInt16("AdminLevel");
                    
                    var loadedPosition = new Position(dataReader.GetFloat("PlayerPosX"), dataReader.GetFloat("PlayerPosY"), dataReader.GetFloat("PlayerPosZ"));
                    myPlayer.PlayerPos = loadedPosition;
                }
            }
        }


        public static void SaveAccount(MyPlayer player)
        {
            MySqlCommand mySqlCommand = _connection.CreateCommand();
            mySqlCommand.CommandText =
                "UPDATE accounts SET playerName=@playerName, money=@money, adminlevel=@adminlevel, playerposx=@playerPosX, playerposy=@playerPosY, playerposz=@playerPosZ WHERE discordId=@discordId";

            mySqlCommand.Parameters.AddWithValue("@playerName", player.PlayerName);
            mySqlCommand.Parameters.AddWithValue("@money", player.Money);
            mySqlCommand.Parameters.AddWithValue("@adminLevel", player.AdminLevel);
            mySqlCommand.Parameters.AddWithValue("@discordId", player.DiscordId);
            mySqlCommand.Parameters.AddWithValue("@playerPosX", player.PlayerPos.X);
            mySqlCommand.Parameters.AddWithValue("@playerPosY", player.PlayerPos.Y);
            mySqlCommand.Parameters.AddWithValue("@playerPosZ", player.PlayerPos.Z);
            
            mySqlCommand.ExecuteNonQuery();
        }
    }
}