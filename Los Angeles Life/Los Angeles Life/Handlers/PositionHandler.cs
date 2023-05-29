using AltV.Net;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using Los_Angeles_Life.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Los_Angeles_Life.Handlers
{
    public abstract class PositionHandler : IScript
    {
        public static void HandlePositionSave(object? state)
        {
            foreach (IPlayer player in Alt.GetAllPlayers())
            {
                Alt.Log(player.Position.ToString());
            }
        }
    }
}
