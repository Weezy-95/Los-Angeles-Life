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
            Alt.OnColShape += OnGarageStorageColShape;
        }

        public static void StopColShapeEventSystem()
        {
            Alt.OnColShape -= OnGarageStorageColShape;
        }

        private static void OnGarageStorageColShape(IColShape colShape, IEntity entity, bool state)
        {
            if (colShape.HasMetaData("Server:ColShape:GarageStoragePosition") && state)
            {
                Position colShapePosition = new Position(colShape.GetPosition().X, colShape.GetPosition().Y, colShape.GetPosition().Z);

                foreach(IColShape listColShape in colShapeList.Values)
                {
                    Position listColShapePosition = new Position(listColShape.GetPosition().X, listColShape.GetPosition().Y, listColShape.GetPosition().Z);

                    if (colShapePosition.Equals(listColShapePosition))
                    {
                        // noch kein Plan wieso weshalb ich das eingebaut habe :D
                    }
                }
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
                    colShapeList.Add(colShapeCounter, colShape);
                    storagePositionList.Add(storagePosition);
                    garageEntry.Value.ColShapeList.Add(colShape);

                    colShapeCounter++;
                }
            }

            // Hier irgendwie die Marker ertellen..
        }
    }
}
