using AltV.Net;
using AltV.Net.Data;
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
    public abstract class PlayerSaveHandler : IScript
    {
        public static void SaveAllPlayersPositions(object? state)
        {
            foreach (IPlayer player in Alt.GetAllPlayers())
            {
                Alt.Log("Vor der Schleife: " + player.IsConnected.ToString());
                if (player.IsConnected)
                {
                    Alt.Log(player.IsConnected.ToString());
                    Position playerPosition = player.Position;
                    Rotation playerRotation = player.Rotation;
                    DatabaseHandler.SaveAllPlayersPositions(player.DiscordId, playerPosition, playerRotation);
                }
            }
        }
    }
}
