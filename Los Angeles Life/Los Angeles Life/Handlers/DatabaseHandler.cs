using AltV.Net;
using MySql.Data.MySqlClient;

namespace Los_Angeles_Life.Handlers
{
    public class DatabaseHandler
    {
        private readonly MySqlConnection _connection;
        
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
    }
}