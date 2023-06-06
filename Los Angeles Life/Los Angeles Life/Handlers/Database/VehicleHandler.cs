using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using Los_Angeles_Life.Entities;
using Los_Angeles_Life.Factions;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace Los_Angeles_Life.Vehicles
{
    abstract class VehicleHandler : IScript
    {
        private static MySqlConnection connection;
        private static Dictionary<long, ServerVehicle> serverVehicleList;
        private static Dictionary<int, VehicleTemplate> vehicleTemplateList;


        const string dbHost = "localhost";
        const string dbPort = "4406";
        const string dbUser = "dev";
        const string dbPassword = "Sonner2021$";
        const string dbName = "altv";

        const string connectionString = $"Server={dbHost};Port={dbPort};Database={dbName};Uid={dbUser};Pwd={dbPassword};";

        public static void LoadVehicleSystem()
        {
            connection = new MySqlConnection(connectionString);
            serverVehicleList = new Dictionary<long, ServerVehicle>();
            vehicleTemplateList = new Dictionary<int, VehicleTemplate>();
            LoadVehicleTemplatesFromDatabase();
            LoadVehiclesFromDatabase();
            SpawnVehiclesInWorldWhenNotStoraged();
        }

        private static void LoadVehicleTemplatesFromDatabase()
        {
            try
            {
                connection.Open();

                MySqlCommand mySqlCommand = connection.CreateCommand();
                mySqlCommand.CommandText = "SELECT * FROM vehicletemplates";
                MySqlDataReader reader = mySqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    VehicleTemplate vehicleTemplate = new VehicleTemplate();

                    vehicleTemplate.Id = reader.GetInt16("Id");
                    vehicleTemplate.Name = reader.GetString("Name");
                    vehicleTemplate.ModelId = reader.GetUInt32("ModelId");
                    vehicleTemplate.Fuel = reader.GetFloat("Fuel");

                    vehicleTemplateList.Add(vehicleTemplate.Id, vehicleTemplate);
                }
            }
            catch (Exception ex)
            {
                Alt.Log("[MySQL] Fehler mit der Faction Abfrage: " + ex);
            }
            finally
            {
                connection.Close();
            }
        }

        private static void LoadVehiclesFromDatabase()
        {
            try
            {
                connection.Open();

                MySqlCommand mySqlCommand = connection.CreateCommand();
                mySqlCommand.CommandText = "SELECT * FROM vehicles";
                MySqlDataReader reader = mySqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    ServerVehicle serverVehicle = new ServerVehicle
                    {
                        Id = reader.GetInt64("Id"),
                        VehicleTemplateId = reader.GetInt16("VehicleTemplateId"),
                        FactionId = reader.GetInt16("FactionId"),
                        GarageStorageId = reader.GetInt16("GarageStorageId"),
                        Owner = reader.GetString("Owner"),
                        Fuel = reader.GetFloat("Fuel"),
                        Mileage = reader.GetFloat("Mileage"),
                        IsEngineHealthy = reader.GetBoolean("IsEngineHealthy"),
                        IsLocked = reader.GetBoolean("IsLocked"),
                        IsInGarage = reader.GetBoolean("IsInGarage")
                    };

                    Position serverVehiclePosition = new Position(
                        reader.GetFloat("PositionX"),
                        reader.GetFloat("PositionY"),
                        reader.GetFloat("PositionZ"));
                    serverVehicle.VehiclePosition = serverVehiclePosition;

                    Rotation serverVehicleRotation = new Rotation(
                        0f,
                        0f,
                        reader.GetFloat("Rotation"));
                    serverVehicle.VehicleRotation = serverVehicleRotation;

                    serverVehicle.Plate = reader.GetString("Plate");

                    serverVehicleList.Add(serverVehicle.Id, serverVehicle);
                }
            }
            catch (Exception ex)
            {
                Alt.Log("[MySQL] Fehler mit der Vehicle Abfrage: " + ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public static void CreateNewVehicle(int carToSpawn, string plate, Position vehicleSpawnPosition, Rotation vehicleRotation, MyPlayer spawner)
        {
            try
            {
                VehicleTemplate vehicleTemplate = vehicleTemplateList[carToSpawn];
                ServerVehicle serverVehicle = new ServerVehicle
                {
                    VehicleTemplateId = vehicleTemplate.Id,
                    FactionId = spawner.FactionId,
                    GarageStorageId = 0,
                    Owner = spawner.DiscordId,
                    Fuel = vehicleTemplate.Fuel,
                    Mileage = 0f,
                    IsEngineHealthy = true,
                    IsLocked = true,
                    IsInGarage = false,
                    VehiclePosition = vehicleSpawnPosition,
                    VehicleRotation = vehicleRotation,
                    Plate = plate
                };

                connection.Open();

                MySqlCommand mySqlCommand = connection.CreateCommand();
                mySqlCommand.CommandText =
                    "INSERT INTO vehicles (VehicleTemplateId, FactionId, GarageStorageId, Owner, Fuel, Mileage, PositionX, PositionY, PositionZ, Rotation, Plate) " +
                    "VALUES (@VehicleTemplateId, @FactionId, @GarageStorageId, @Owner, @Fuel, @Mileage, @PositionX, @PositionY, @PositionZ, @Rotation, @Plate)";
                mySqlCommand.Parameters.AddWithValue("@VehicleTemplateId", serverVehicle.VehicleTemplateId);
                mySqlCommand.Parameters.AddWithValue("@FactionId", serverVehicle.FactionId);
                mySqlCommand.Parameters.AddWithValue("@GarageStorageId", serverVehicle.GarageStorageId);
                mySqlCommand.Parameters.AddWithValue("@Owner", serverVehicle.Owner);
                mySqlCommand.Parameters.AddWithValue("@Fuel", serverVehicle.Fuel);
                mySqlCommand.Parameters.AddWithValue("@Mileage", serverVehicle.Mileage);
                mySqlCommand.Parameters.AddWithValue("@PositionX", serverVehicle.VehiclePosition.X);
                mySqlCommand.Parameters.AddWithValue("@PositionY", serverVehicle.VehiclePosition.Y);
                mySqlCommand.Parameters.AddWithValue("@PositionZ", serverVehicle.VehiclePosition.Z);
                mySqlCommand.Parameters.AddWithValue("@Rotation", serverVehicle.VehicleRotation.Yaw);
                mySqlCommand.Parameters.AddWithValue("@Plate", serverVehicle.Plate);

                mySqlCommand.ExecuteNonQuery();

                serverVehicle.Id = mySqlCommand.LastInsertedId;

                IVehicle spawnedVehicle = Alt.CreateVehicle(vehicleTemplateList[carToSpawn].ModelId, vehicleSpawnPosition, vehicleRotation);
                serverVehicle.SessionId = spawnedVehicle.Id;

                MySqlCommand updateFactionNameCommand = connection.CreateCommand();
                updateFactionNameCommand.CommandText = "UPDATE vehicles SET SessionId = @SessionId WHERE Owner = @Owner AND Id = @Id";
                updateFactionNameCommand.Parameters.AddWithValue("@SessionId", serverVehicle.SessionId);
                updateFactionNameCommand.Parameters.AddWithValue("@Owner", spawner.DiscordId);
                updateFactionNameCommand.Parameters.AddWithValue("@Id", serverVehicle.Id);
                updateFactionNameCommand.ExecuteNonQuery();

                serverVehicleList.Add(serverVehicle.Id, serverVehicle);
                spawner.SetIntoVehicle(spawnedVehicle, 1); // Testzwecke
            }
            catch (Exception ex)
            {
                Alt.Log("[MySQL] Fehler beim Erstellen des Vehicles: " + ex);
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public static void SaveVehicle(IVehicle vehicleToSave, IPlayer player, byte seat)
        {
            try
            {
                if (seat != 1) { return; }

                long vehicleIdToSave = 0;

                connection.Open();

                MySqlCommand mySqlCommand = connection.CreateCommand();
                mySqlCommand.CommandText = "SELECT Id FROM vehicles WHERE owner=@owner AND sessionId=@sessionId";

                mySqlCommand.Parameters.AddWithValue("@owner", player.DiscordId);
                mySqlCommand.Parameters.AddWithValue("@sessionId", vehicleToSave.Id);

                using (MySqlDataReader dataReader = mySqlCommand.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        dataReader.Read();

                        vehicleIdToSave = dataReader.GetInt64("Id");
                    }
                };

                ServerVehicle serverVehicle = serverVehicleList[vehicleIdToSave];

                serverVehicle.VehiclePosition = vehicleToSave.Position;
                serverVehicle.VehicleRotation = vehicleToSave.Rotation;

                mySqlCommand = connection.CreateCommand();
                mySqlCommand.CommandText =
                    "UPDATE vehicles SET GarageStorageId = @GarageStorageId, Fuel = @Fuel, Mileage = @Mileage, " +
                    "IsEngineHealthy = @IsEngineHealthy, IsLocked = @IsLocked, IsInGarage = @IsInGarage, " +
                    "PositionX = @PositionX, PositionY = @PositionY, PositionZ = @PositionZ, Rotation = @Rotation, Plate = @Plate " +
                    "WHERE Owner = @Owner AND Id = @Id";
                mySqlCommand.Parameters.AddWithValue("@GarageStorageId", serverVehicle.GarageStorageId);
                mySqlCommand.Parameters.AddWithValue("@Fuel", serverVehicle.Fuel);
                mySqlCommand.Parameters.AddWithValue("@Mileage", serverVehicle.Mileage);
                mySqlCommand.Parameters.AddWithValue("@IsEngineHealthy", serverVehicle.IsEngineHealthy);
                mySqlCommand.Parameters.AddWithValue("@IsLocked", serverVehicle.IsLocked);
                mySqlCommand.Parameters.AddWithValue("@IsInGarage", serverVehicle.IsInGarage);
                mySqlCommand.Parameters.AddWithValue("@PositionX", serverVehicle.VehiclePosition.X);
                mySqlCommand.Parameters.AddWithValue("@PositionY", serverVehicle.VehiclePosition.Y);
                mySqlCommand.Parameters.AddWithValue("@PositionZ", serverVehicle.VehiclePosition.Z);
                mySqlCommand.Parameters.AddWithValue("@Rotation", serverVehicle.VehicleRotation.Yaw);
                mySqlCommand.Parameters.AddWithValue("@Plate", serverVehicle.Plate);
                mySqlCommand.Parameters.AddWithValue("@Owner", player.DiscordId);
                mySqlCommand.Parameters.AddWithValue("@Id", vehicleIdToSave);

                mySqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Alt.Log("[MySQL] Fehler beim Speichern des Vehicles: " + ex);
                throw;
            }
            finally
            {
                connection.Close();
            }

        }

        private static void SpawnVehiclesInWorldWhenNotStoraged()
        {
            try
            {
                connection.Open();

                foreach (KeyValuePair<long, ServerVehicle> serverVehicle in serverVehicleList)
                {
                    if (serverVehicle.Value.IsInGarage) { return; }

                    IVehicle spawnedVehicle = Alt.CreateVehicle(vehicleTemplateList[serverVehicle.Value.VehicleTemplateId].ModelId, serverVehicle.Value.VehiclePosition, serverVehicle.Value.VehicleRotation);
                    serverVehicle.Value.SessionId = spawnedVehicle.Id;

                    MySqlCommand updateFactionNameCommand = connection.CreateCommand();
                    updateFactionNameCommand.CommandText = "UPDATE vehicles SET SessionId = @SessionId WHERE Owner = @Owner AND Id = @Id";
                    updateFactionNameCommand.Parameters.AddWithValue("@SessionId", serverVehicle.Value.SessionId);
                    updateFactionNameCommand.Parameters.AddWithValue("@Owner", serverVehicle.Value.Owner);
                    updateFactionNameCommand.Parameters.AddWithValue("@Id", serverVehicle.Value.Id);
                    updateFactionNameCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Alt.Log("[MySQL] Fehler beim Hinzufügen des Vehicles: " + ex);
                throw;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
