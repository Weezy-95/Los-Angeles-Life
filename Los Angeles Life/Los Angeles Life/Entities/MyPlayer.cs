using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using System.Runtime.CompilerServices;

namespace Los_Angeles_Life.Entities
{
    public class MyPlayer : Player
    {
        public int PlayerId { get; set; }
        public new long DiscordId { get; set; }
        public string PlayerName { get; set; }
        public ulong SocialClub { get; set; }
        public long Money { get; set; }
        public int AdminLevel { get; set; }

        // Hier das Objekt "Position" speichern. Aus der Position können wir die Werte abrufen (X, Y, Z, yaw für Rotation);
        public Position PlayerPos { get; set; }
        public bool IsAduty { get; set; }
        public bool IsWhitelisted { get; set; }
        public bool IsConnected { get; set; }
        
        public MyPlayer(ICore core, IntPtr nativePointer, ushort id) : base(core, nativePointer, id)
        {
        }

        public void LastCharacterPos(MyPlayer player, Position newPosition)
        {
            PlayerPos = newPosition;
            Alt.Log("LastCharacterPos: " + PlayerPos);
        }
    }
}