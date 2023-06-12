using AltV.Net;
using AltV.Net.Elements.Entities;
using Los_Angeles_Life_Server.Entities;

namespace Los_Angeles_Life_Server.Factories;

public class MyPlayerFactory : IEntityFactory<IPlayer>
{
    public IPlayer Create(ICore core, IntPtr entityPointer, ushort id)
    {
        return new MyPlayer(core, entityPointer, id);
    }
}