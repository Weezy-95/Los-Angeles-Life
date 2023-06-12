using AltV.Net;
using AltV.Net.Elements.Entities;
using Los_Angeles_Life_Server.Entities;

namespace Los_Angeles_Life_Server.Factories;

public class MyVehicleFactory : IEntityFactory<IVehicle>
{
    public IVehicle Create(ICore core, IntPtr entityPointer, ushort id)
    {
        return new MyVehicle(core, entityPointer, id);
    }
}