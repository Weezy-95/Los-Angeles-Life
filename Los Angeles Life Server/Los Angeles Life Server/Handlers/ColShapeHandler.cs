using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using Los_Angeles_Life_Server.Entities;
using Los_Angeles_Life_Server.Garages;
using Los_Angeles_Life_Server.Misc;

namespace Los_Angeles_Life_Server.Handlers
{
    abstract class ColShapeHandler : IScript
    {
        public static Dictionary<int, IColShape> colShapeList = new Dictionary<int, IColShape>();
        private static int colShapeCounter = 1;

        public static void LoadingColShapeEventSystem()
        {
            Alt.OnColShape += OnVehicleEnterColShape;
        }

        private static void OnVehicleEnterColShape(IColShape colShape, IEntity entity, bool state)
        {
            if(!(entity is IVehicle)) { return; }

            IVehicle vehicle = (IVehicle)entity;

            if (colShape.HasMetaData("Server:ColShape:GarageStoragePosition"))
            {
                GarageHandler.AddOrRemoveVehiclesToStoreOnGarage(colShape, vehicle, state);
            }
        }

        public static void CreateGarageStorageColShapesAndMarker()
        {
            List<Position> storagePositionList = new List<Position>();

            foreach (KeyValuePair<int, Garage> garageEntry in GarageHandler.garageList)
            {
                Alt.Log("Garage gefunden: " + garageEntry.Value.Name);

                foreach (SpawnInformation spawnInformation in garageEntry.Value.StoragePositionInformationList)
                {
                    Position storagePosition = spawnInformation.Position;
                    storagePosition.Z -= 1f;

                    IColShape colShape = Alt.CreateColShapeCylinder(storagePosition, 3f, 3f);
                    colShape.IsPlayersOnly = false;
                    colShape.SetMetaData("Server:ColShape:GarageStoragePosition", "GarageStoragePosition");
                    colShape.SetMetaData("Server:ColShape:Garage:" + garageEntry.Value.Name, garageEntry.Value.Name);
                    colShapeList.Add(colShapeCounter, colShape);
                    storagePositionList.Add(storagePosition);

                    colShapeCounter++;
                }
            }

            // Hier irgendwie die Marker ertellen..
        }
    }
}
