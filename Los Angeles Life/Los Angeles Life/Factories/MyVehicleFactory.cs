using AltV.Net;
using AltV.Net.Elements.Entities;
using Los_Angeles_Life.Entities;

namespace Los_Angeles_Life.Factories;

public class MyVehicleFactory : IEntityFactory<IVehicle>
{
    public IVehicle Create(ICore core, IntPtr entityPointer, ushort id)
    {
        return new MyVehicle(core, entityPointer, id);
    }
}