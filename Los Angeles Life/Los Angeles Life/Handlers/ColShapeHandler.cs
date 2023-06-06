using AltV.Net;
using AltV.Net.Elements.Entities;
using AltV.Net.Events;
using AltV.Net.Shared.Elements.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Los_Angeles_Life.Handlers
{
    abstract class ColShapeHandler : IScript
    {
        public static void LoadingColShapeEvents()
        {
            Alt.OnColShape += OnGarageStorageColShape;
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
    }
}
