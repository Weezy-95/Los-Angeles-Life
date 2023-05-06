using AltV.Net;
using AltV.Net.Elements.Entities;

namespace Los_Angeles_Life.Entities
{
    public class MyPlayer : Player
    {
        public int PlayerId { get; set; }
        public string Username { get; set; }
        public uint SocialClub { get; set; }
        public int AdminLevel { get; set; }
        public bool IsAduty { get; set; }
        public bool IsWhitelisted { get; set; }
        
        public MyPlayer(ICore core, IntPtr nativePointer, ushort id) : base(core, nativePointer, id)
        {
        }
    }
}