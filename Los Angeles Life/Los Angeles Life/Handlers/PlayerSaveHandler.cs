using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;

namespace Los_Angeles_Life.Handlers
{
    public abstract class PlayerSaveHandler : IScript
    {
        public static void SaveAllPlayersPositions(object? state)
        {
            foreach (IPlayer player in Alt.GetAllPlayers())
            {
                Position playerPosition = player.Position;
                Rotation playerRotation = player.Rotation;
                DatabaseHandler.SaveAllPlayersPositions(player.DiscordId, playerPosition, playerRotation);
                Alt.Log("Position: " + playerPosition + " Rotation: " + playerRotation);
            }
        }
    }
}
