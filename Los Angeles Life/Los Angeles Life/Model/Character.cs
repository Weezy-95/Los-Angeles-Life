using AltV.Net;
using AltV.Net.Data;
using Los_Angeles_Life.Entities;

namespace Los_Angeles_Life.Model
{
    public class Character : IScript
    {
        // Habe es nicht geschafft weiterzumachen, maybe müssen wir das doch einzeln in der DB speichern
        // Könnte dieses Script nicht auch ebenfalls in MyPlayer sein? 
        public static void LastCharacterPos(MyPlayer player, Position newPosition)
        {
            player.PlayerLastPosX = newPosition.X;
            player.PlayerLastPosY = newPosition.Y;
            player.PlayerLastPosZ = newPosition.Z;

            player.PlayerPos = player.PlayerLastPosX + player.PlayerLastPosY + player.PlayerLastPosZ;
            Alt.Log("LastCharacterPos: " + player.PlayerPos);
        }
    }
}