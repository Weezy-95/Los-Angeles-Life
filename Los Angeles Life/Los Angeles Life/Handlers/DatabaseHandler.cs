using AltV.Net;
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
            const string dbPassword = "163540";
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
            mySqlCommand.CommandText = "SELECT * FROM accounts WHERE discordId=@discordId LIMIT 1";
            mySqlCommand.Parameters.AddWithValue("@discordId", discordId);

            using MySqlDataReader dataReader = mySqlCommand.ExecuteReader();
            if (dataReader.HasRows)
            {
                return true;
            }

            return false;
        }

        public static int CreateAccount(String playerName, int discordId)
        {
            try
            {
                MySqlCommand mySqlCommand = _connection.CreateCommand();
                mySqlCommand.CommandText =
                    "INSERT INTO accounts (PlayerName, DiscordId, DiscordName) VALUES (@playerName, @discordId, @discordName)";

                mySqlCommand.Parameters.AddWithValue("@playerName", playerName);
                mySqlCommand.Parameters.AddWithValue("@discordId", discordId);
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
            mySqlCommand.CommandText = "SELECT * FROM accounts WHERE discordId=@discordId LIMT 1";

            mySqlCommand.Parameters.AddWithValue("@discordId", myPlayer.DiscordId);

            using (MySqlDataReader dataReader = mySqlCommand.ExecuteReader())
            {
                if (dataReader.HasRows)
                {
                    dataReader.Read();
                    myPlayer.PlayerId = dataReader.GetInt32("PlayerID");
                    myPlayer.DiscordId = dataReader.GetInt32("DiscordID");
                    myPlayer.DiscordName = dataReader.GetString("DiscordName");
                    myPlayer.PlayerName = dataReader.GetString("PlayerName");
                    myPlayer.SocialClub = dataReader.GetString("SocialClub");
                    myPlayer.Money = dataReader.GetInt32("Money");
                    myPlayer.AdminLevel = dataReader.GetInt16("AdminLevel");
                }
            }
        }

        public static void SaveAccount(MyPlayer myPlayer)
        {
            MySqlCommand mySqlCommand = _connection.CreateCommand();
            mySqlCommand.CommandText =
                "UPDATE accounts SET playerName=@playerName, money=@money, adminlevel=@adminlevel WHERE discordId=@discordId";

            mySqlCommand.Parameters.AddWithValue("@playerName", myPlayer.PlayerName);
            mySqlCommand.Parameters.AddWithValue("@money", myPlayer.Money);
            mySqlCommand.Parameters.AddWithValue("@adminLevel", myPlayer.AdminLevel);
            mySqlCommand.Parameters.AddWithValue("@discordId", myPlayer.DiscordId);
            mySqlCommand.Parameters.AddWithValue("@discordName", myPlayer.DiscordName);
        }
    }
}