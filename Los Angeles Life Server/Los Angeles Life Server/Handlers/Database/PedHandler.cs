using AltV.Net;
using AltV.Net.Data;
using Los_Angeles_Life_Server.Entities;
using MySql.Data.MySqlClient;

namespace Los_Angeles_Life_Server.Handlers.Database;

public abstract class PedHandler : IScript
{
    private static MySqlConnection connection;
    public static Dictionary<int, MyPed> loadedPedList;

    const string dbHost = "localhost";
    const string dbPort = "4406";
    const string dbUser = "dev";
    const string dbPassword = "Sonner2021$";
    const string dbName = "altv";

    const string connectionString = $"Server={dbHost};Port={dbPort};Database={dbName};Uid={dbUser};Pwd={dbPassword};";
    

    public static void LoadPedSystem()
    {
        connection = new MySqlConnection(connectionString);
        loadedPedList = new Dictionary<int, MyPed>();
        LoadPedCount();
    }
    
    private static void LoadPedCount()
    {
        try
        {
            connection.Open();

            var mySqlCommand = connection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM peds";

            var count = 0;
            using (MySqlDataReader reader = mySqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    var id = reader.GetInt32("Id");
                    count++;
                }
            }

            Alt.Log($"[MySQL] Es wurden erfolgreich {count} Peds geladen!");
        }
        catch (MySqlException ex)
        {
            Alt.Log("[MySQL] Fehler beim Laden des Ped Counts! Grund: " + ex);
            throw;
        }
        finally
        {
            connection.Close();
        }
    }

    public static void LoadPeds(MyPlayer player)
    {
        try
        {
            connection.Open();

            MySqlCommand mySqlCommand = connection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM peds";
            MySqlDataReader reader = mySqlCommand.ExecuteReader();

            while (reader.Read())
            {
                int pedId = reader.GetInt32("Id");

                if (loadedPedList.ContainsKey(pedId))
                    continue;

                MyPed ped = new MyPed();

                ped.Id = pedId;
                ped.Type = reader.GetInt32("Type");
                ped.Name = reader.GetString("Name");
                ped.Hash = reader.GetString("Hash");
                Position loadedPosition = new Position(
                    reader.GetFloat("PositionX"),
                    reader.GetFloat("PositionY"),
                    reader.GetFloat("PositionZ"));

                Rotation loadedRotation = new Rotation(
                    0f,
                    0f,
                    reader.GetFloat("Rotation"));

                ped.Position = loadedPosition;
                ped.Rotation = loadedRotation;

                loadedPedList.Add(pedId, ped);

                bool isNewPedInstance = true;

                player.Emit("Client:Ped:Create", ped.Type, ped.Hash, ped.Position.X, ped.Position.Y, ped.Position.Z, ped.Rotation.Yaw, isNewPedInstance);
            }
        }
        catch (MySqlException ex)
        {
            Alt.Log("[MySQL] Fehler beim Laden der Server Peds: " + ex);
            throw;
        }
        finally
        {
            connection.Close();
        }
    }



    public static void CreatePeds(string name, int type, string hash, float positionX, float positionY, float positionZ, float rotation)
    {
        try
        {
            connection.Open();

            MySqlCommand mySqlCommand = connection.CreateCommand();
            mySqlCommand.CommandText = "INSERT peds SET name=@name, type=@Type, hash=@hash, " +
                                       "positionX=@positionX, positionY=@positionY, positionZ=@positionZ, rotation=@rotation";

            mySqlCommand.Parameters.AddWithValue("@name", name);
            mySqlCommand.Parameters.AddWithValue("@type", type);
            mySqlCommand.Parameters.AddWithValue("@hash", hash);
            mySqlCommand.Parameters.AddWithValue("@positionX", positionX);
            mySqlCommand.Parameters.AddWithValue("@positionY", positionY);
            mySqlCommand.Parameters.AddWithValue("@positionZ", positionZ);
            mySqlCommand.Parameters.AddWithValue("@rotation", rotation);

            mySqlCommand.ExecuteNonQuery();
        }
        catch (MySqlException ex)
        {
            Alt.Log("[MySQL] Fehler beim Speichern des Peds: " + ex);
            throw;
        }
        finally
        {
            connection.Close();
        }
    }

}