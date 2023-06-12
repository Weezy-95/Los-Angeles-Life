using AltV.Net.Data;

namespace Los_Angeles_Life_Server.Entities;

public class MyPed 
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Type { get; set; }
    public string Hash { get; set; }
    public Position Position { get; set; }
    public Rotation Rotation { get; set; }
}