using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using Los_Angeles_Life.Entities;
using Los_Angeles_Life.Garages;
using Los_Angeles_Life.Handlers.Database;
using Los_Angeles_Life.Misc;


namespace Los_Angeles_Life.Handlers
{
    abstract class ColShapeHandler : IScript
    {
        public static void LoadingColShapeEventSystem()
        {
            Alt.OnColShape += OnGarageStorageColShape;
            Alt.Log("Hinzugefügt");
        }

        public static void StopColShapeEventSystem()
        {
            Alt.OnColShape -= OnGarageStorageColShape;
            Alt.Log("Entfernt");
        }

        private static void OnGarageStorageColShape(IColShape colShape, IEntity entity, bool state)
        {
            if (colShape.HasMetaData("Server:ColShape:GarageStoragePosition") && state)
            {
                Alt.Log("Open GarageMenü");
            }
            else
            {
                Alt.Log("Close GarageMenü");
            }
        }

        public static void CreateGarageStorageColShapesAndMarker(MyPlayer player)
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
                    colShape.IsPlayersOnly = true;
                    colShape.SetMetaData("Server:ColShape:GarageStoragePosition", "GarageStoragePosition");

                    storagePositionList.Add(storagePosition);

                    Alt.Log("ColShape von " + garageEntry.Value.Name + " für SpawnGarageStoragePosition erstellt.");
                }
            }

            player.Emit("Client:Marker:Garage", storagePositionList);
        }
    }
}
