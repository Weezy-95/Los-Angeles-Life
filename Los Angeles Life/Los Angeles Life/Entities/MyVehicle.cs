using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;

namespace Los_Angeles_Life.Entities;

public class MyVehicle : Vehicle
{
    public MyVehicle(ICore core, uint model, Position position, Rotation rotation) : base(core, model, position, rotation)
    {
    }

    public MyVehicle(ICore core, IntPtr nativePointer, ushort id) : base(core, nativePointer, id)
    {
    }
}