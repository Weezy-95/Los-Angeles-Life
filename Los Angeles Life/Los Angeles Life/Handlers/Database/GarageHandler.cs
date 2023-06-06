using AltV.Net;
using AltV.Net.Data;
using Los_Angeles_Life.Misc;
using MySql.Data.MySqlClient;

namespace Los_Angeles_Life.Garages
{
    abstract class GarageHandler
    {
        private static MySqlConnection connection;
        public static Dictionary<int, Garage> garageList;

        const string dbHost = "localhost";
        const string dbPort = "4406";
        const string dbUser = "dev";
        const string dbPassword = "Sonner2021$";
        const string dbName = "altv";

        const string connectionString = $"Server={dbHost};Port={dbPort};Database={dbName};Uid={dbUser};Pwd={dbPassword};";

        public static void LoadGarageSystem()
        {
            connection = new MySqlConnection(connectionString);
            garageList = new Dictionary<int, Garage>();
            LoadGarages();
            LoadGarageSpawnPositionsAndGarageStoragePositions();
        }

        private static void LoadGarages()
        {
            try
            {
                connection.Open();

                MySqlCommand mySqlCommand = connection.CreateCommand();
                mySqlCommand.CommandText = "SELECT * FROM garages";
                MySqlDataReader reader = mySqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32("Id");
                    string name = reader.GetString("Name");
                    Position location = new Position(
                        reader.GetFloat("LocationX"),
                        reader.GetFloat("LocationY"),
                        reader.GetFloat("LocationZ"));
                    
                    int blipId = reader.GetInt32("BlipId");
                    int blipColorId = reader.GetInt32("BlipColorId");

                    Garage garage = new Garage(id, name, location, blipId, blipColorId);

                    garageList.Add(id, garage);
                    Alt.Log("Garage: " + garage.Name);
                }
            }
            catch (Exception ex)
            {
                Alt.Log("[MySQL] Fehler mit dem Garage Abfrage: " + ex);
            }
            finally
            {
                connection.Close();
            }
        }

        private static void LoadGarageSpawnPositionsAndGarageStoragePositions()
        {
            try
            {
                connection.Open();

                MySqlCommand mySqlCommand = connection.CreateCommand();
                
                foreach (KeyValuePair<int, Garage> garageEntry in garageList)
                {
                    mySqlCommand.CommandText = "SELECT * FROM garagespawnpositions WHERE GarageId = @GarageId";
                    mySqlCommand.Parameters.AddWithValue("@GarageId", garageEntry.Value.Id);
                    MySqlDataReader reader = mySqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        Position position = new Position(
                            reader.GetFloat("PositionX"),
                            reader.GetFloat("PositionY"),
                            reader.GetFloat("PositionZ"));

                        Rotation rotation = new Rotation(
                            0f,
                            0f,
                            reader.GetFloat("Rotation"));

                        SpawnInformation spawnInformation = new SpawnInformation(position, rotation);

                        garageEntry.Value.SpawnPositionInformationList.Add(spawnInformation);

                        Alt.Log("GarageSpawnPosition: " + spawnInformation.Position);
                    }

                    reader.Close();

                    mySqlCommand.CommandText = "SELECT * FROM garagestoragepositions WHERE GarageId = @GarageId";
                    reader = mySqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        Position position = new Position(
                            reader.GetFloat("PositionX"),
                            reader.GetFloat("PositionY"),
                            reader.GetFloat("PositionZ"));

                        Rotation rotation = new Rotation(
                            0f,
                            0f,
                            reader.GetFloat("Rotation"));

                        SpawnInformation spawnInformation = new SpawnInformation(position, rotation);

                        garageEntry.Value.StoragePositionInformationList.Add(spawnInformation);

                        Alt.Log("GarageStoragePosition: " + spawnInformation.Position);
                    }
                }
            }
            catch (Exception ex)
            {
                Alt.Log("[MySQL] Fehler mit dem Garage Abfrage: " + ex);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
