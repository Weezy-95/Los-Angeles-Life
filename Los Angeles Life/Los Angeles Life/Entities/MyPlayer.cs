using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;

namespace Los_Angeles_Life.Entities
{
    public class MyPlayer : Player
    {
        public int PlayerId { get; set; }
        public new string DiscordId { get; set; }
        public string PlayerName { get; set; }
        public ulong SocialClub { get; set; }
        public bool IsWhitelisted { get; set; }
        public int AdminLevel { get; set; }
        public bool IsAduty { get; set; }
        public float Cash { get; set; }
        public float Bank { get; set; }
        public int FactionId { get; set; }
        public Position PlayerPos { get; set; }
        public Rotation PlayerRot { get; set; }
        public int PlayerDimension { get; set; }
        

        public MyPlayer(ICore core, IntPtr nativePointer, ushort id) : base(core, nativePointer, id)
        {
        }
    }
}