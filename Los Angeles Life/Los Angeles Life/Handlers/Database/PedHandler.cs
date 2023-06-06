using AltV.Net;
using AltV.Net.Data;
using Los_Angeles_Life.Entities;
using MySql.Data.MySqlClient;

namespace Los_Angeles_Life.Handlers.Database;

public abstract class PedHandler : IScript
{
    private static MySqlConnection connection;
    
    const string dbHost = "localhost";
    const string dbPort = "4406";
    const string dbUser = "dev";
    const string dbPassword = "Sonner2021$";
    const string dbName = "altv";

    const string connectionString = $"Server={dbHost};Port={dbPort};Database={dbName};Uid={dbUser};Pwd={dbPassword};";
    

    public static void LoadPedSystem()
    {
        connection = new MySqlConnection(connectionString);
        LoadPedCount();
        LoadPeds();
    }
    
    private static void LoadPedCount()
    {
        try
        {
            connection.Open();

            var mySqlCommand = connection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM ped";

            var count = 0;
            using (MySqlDataReader reader = mySqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    var id = reader.GetInt32("PedId");
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

    public static void LoadPeds()
    {
        try
        {
            connection.Open();

            MySqlCommand mySqlCommand = connection.CreateCommand();
            mySqlCommand.CommandText = "SELECT * FROM ped";
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            
            while (reader.Read())
            {
                MyPed ped = new();

                ped.PedId = reader.GetInt32("PedId");
                ped.PedType = reader.GetInt32("PedType");
                ped.PedName = reader.GetString("PedName");
                ped.PedHash = reader.GetString("PedHash");
                Position loadedPosition = new Position(
                    reader.GetFloat("PedPosX"),
                    reader.GetFloat("PedPosY"),
                    reader.GetFloat("PedPosZ"));

                Rotation loadedRotation = new Rotation(
                    0f,
                    0f,
                    reader.GetFloat("PedRot"));

                ped.PedPos = loadedPosition;
                ped.PedRot = loadedRotation;
                
                Alt.EmitAllClients("Client:Ped:Create", ped.PedType, ped.PedHash, ped.PedPos.X, ped.PedPos.Y, ped.PedPos.Z, ped.PedRot.Yaw);
            }
        }
        catch (MySqlException ex)
        {
            Alt.Log("[MySQL] Fehler beim laden der Server Ped's: " + ex);
            throw;
        }
        finally
        {
            connection.Close();
        }
    }

    public static void CreatePeds(string pedName, int pedType, string pedHash, float pedPosX, float pedPosY, float pedPosZ, float pedRot)
    {
        try
        {
            connection.Open();

            MySqlCommand mySqlCommand = connection.CreateCommand();
            mySqlCommand.CommandText = "INSERT ped SET pedName=@pedName, pedType=@pedType, pedHash=@pedHash, " +
                                       "pedPosX=@pedPosX, pedPosY=@pedPosY, pedPosZ=@pedPosZ, pedRot=@pedRot";

            mySqlCommand.Parameters.AddWithValue("@pedName", pedName);
            mySqlCommand.Parameters.AddWithValue("@pedType", pedType);
            mySqlCommand.Parameters.AddWithValue("@pedHash", pedHash);
            mySqlCommand.Parameters.AddWithValue("@pedPosX", pedPosX);
            mySqlCommand.Parameters.AddWithValue("@pedPosY", pedPosY);
            mySqlCommand.Parameters.AddWithValue("@pedPosZ", pedPosZ);
            mySqlCommand.Parameters.AddWithValue("@pedRot", pedRot);

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