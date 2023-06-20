using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using Los_Angeles_Life_Server.Entities;
using Los_Angeles_Life_Server.Garages;
using Los_Angeles_Life_Server.Misc;
using Los_Angeles_Life_Server.Vehicles;
using MySql.Data.MySqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace Los_Angeles_Life_Server.Handlers
{
    abstract class GarageHandler
    {
        public static Dictionary<int, Garage> garageList;

        public static void LoadGarageSystem()
        {
            garageList = new Dictionary<int, Garage>();
            LoadGarages();
            LoadGarageSpawnPositionsAndGarageStoragePositions();
        }

        private static void LoadGarages()
        {
            try
            {
                MySqlConnection connection = DatabaseHandler.OpenConnection();

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
                    Alt.Log("[MySQL] Es wurde erfolgreich die Garage " + garage.Name + " geladen!");
                }
            }
            catch (Exception ex)
            {
                Alt.Log("[MySQL] Fehler mit dem Garage Abfrage: " + ex);
            }
            finally
            {
                DatabaseHandler.CloseConnection();
            }
        }

        private static void LoadGarageSpawnPositionsAndGarageStoragePositions()
        {
            try
            {
                MySqlConnection connection = DatabaseHandler.OpenConnection();

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
                    }
                }
            }
            catch (Exception ex)
            {
                Alt.Log("[MySQL] Fehler mit dem Garage Abfrage: " + ex);
            }
            finally
            {
                DatabaseHandler.CloseConnection();
            }
        }

        private static void GetVehiclesFromPlayerByGarage(MyPlayer player, int garageId)
        {
            Dictionary<long, ServerVehicle> vehicleList = new Dictionary<long, ServerVehicle>();

            try
            {
                Alt.Log("GetVehiclesFromPlayerByGarage");
                MySqlConnection connection = DatabaseHandler.OpenConnection();

                MySqlCommand mySqlCommand = connection.CreateCommand();

                mySqlCommand.CommandText =
                    "SELECT * " +
                    "FROM vehicles " +
                    "JOIN garagestorages " +
                    "ON vehicles.GarageStorageId = garagestorages.Id " +
                    "WHERE garagestorages.GarageId = @GarageId AND vehicles.Owner = @Owner";

                mySqlCommand.Parameters.AddWithValue("@GarageId", garageId);
                mySqlCommand.Parameters.AddWithValue("@Owner", player.DiscordId);
                MySqlDataReader reader = mySqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    ServerVehicle vehicle = new ServerVehicle();
                    vehicle.Id = reader.GetInt64("Id");
                    vehicle.SessionId = reader.GetInt32("SessionId");
                    vehicle.VehicleTemplateId = reader.GetInt32("VehicleTemplateId");
                    vehicle.FactionId = reader.GetInt32("FactionId");
                    vehicle.GarageStorageId = reader.GetInt32("GarageStorageId");
                    vehicle.Owner = reader.GetString("Owner");
                    vehicle.Fuel = reader.GetFloat("Fuel");
                    vehicle.Mileage = reader.GetFloat("Mileage");
                    vehicle.IsEngineHealthy = true;
                    vehicle.IsLocked = true;
                    vehicle.IsInGarage = true;
                    vehicle.Plate = reader.GetString("Plate");

                    vehicleList.Add(vehicle.Id, vehicle);
                }

                foreach (KeyValuePair<long, ServerVehicle> entry in vehicleList)
                {
                    Alt.Log(entry.Value.Id.ToString() + " " + entry.Value.GarageStorageId.ToString());
                }
            }
            catch (MySqlException ex)
            {
                Alt.Log("[MySQL] Fehler mit der Garage-Abfrage: " + ex);
            }
            finally
            {
                DatabaseHandler.CloseConnection();
            }
        }

        public static void AddOrRemoveVehiclesToStoreOnGarage(IColShape colShape, IVehicle vehicle, bool state)
        {
            foreach (KeyValuePair<int, Garage> garage in garageList)
            {
                string garageName;

                colShape.GetMetaData("Server:ColShape:Garage:" + garage.Value.Name, out garageName);

                if (garage.Value.Name.Equals(garageName))
                {
                    if (state)
                    {
                        garage.Value.VehiclesToStore.Add(vehicle);
                    }
                    else
                    {
                        garage.Value.VehiclesToStore.Remove(vehicle);
                    }
                }

                return;
            }
        }

        public static void GetPlayerInformationFromGarage()
        {
            Alt.OnClient<int>("Client:Garage:SendPlayerInformation", (player, garageId) =>
            {
            });
        }

        public static void RequestToParkInVehicle()
        {
            Alt.OnClient<int>("Client:Garage:ParkIntoGarage", (player, garageId) =>
            {
            });
        }
    }
}
