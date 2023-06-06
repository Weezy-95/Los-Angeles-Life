using AltV.Net.Data;

namespace Los_Angeles_Life.Entities;

public class MyPed 
{
    public int PedId { get; set; }
    public string PedName { get; set; }
    public int PedType { get; set; }
    public string PedHash { get; set; }
    public Position PedPos { get; set; }
    public Rotation PedRot { get; set; }
}