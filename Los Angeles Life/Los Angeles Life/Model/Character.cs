using AltV.Net;
using AltV.Net.Data;
using Los_Angeles_Life.Entities;

namespace Los_Angeles_Life.Model
{
    public class Character : IScript
    {
        // Habe es nicht geschafft weiterzumachen, maybe müssen wir das doch einzeln in der DB speichern
        public static void LastCharacterPos(MyPlayer player, Position lastPosition)
        {
            float playerPosX = lastPosition.X;
            float playerPosY = lastPosition.Y;
            float playerPosZ = lastPosition.Z;
            
        }
    }
}