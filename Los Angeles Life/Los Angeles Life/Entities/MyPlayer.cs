using AltV.Net;
using AltV.Net.Elements.Entities;

namespace Los_Angeles_Life.Entities
{
    public class MyPlayer : Player
    {
        public int PlayerId { get; set; }
        public new long DiscordId { get; set; }
        public string PlayerName { get; set; }
        public string SocialClub { get; set; }
        public long Money { get; set; }
        public int AdminLevel { get; set; }
        public bool IsAduty { get; set; }
        public bool IsWhitelisted { get; set; }
        public bool IsLoggedIn { get; set; }
        
        public MyPlayer(ICore core, IntPtr nativePointer, ushort id) : base(core, nativePointer, id)
        {
            Money = 0;
            AdminLevel = 0;
            IsLoggedIn = false;
        }
    }
}