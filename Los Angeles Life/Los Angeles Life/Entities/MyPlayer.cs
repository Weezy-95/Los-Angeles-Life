using AltV.Net;
using AltV.Net.Elements.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Los_Angeles_Life.Entities;

public class MyPlayer : Player
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string DiscordId { get; set; }
    public string SocialClub { get; set; }
    
    public MyPlayer(ICore core, IntPtr nativePointer, ushort id) : base(core, nativePointer, id)
    {
    }
}