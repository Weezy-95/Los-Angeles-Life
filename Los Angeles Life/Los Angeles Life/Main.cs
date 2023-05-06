using AltV.Net;
using AltV.Net.Elements.Entities;
using Los_Angeles_Life.Factories;

namespace Los_Angeles_Life;

internal class Main : Resource
{
    public override void OnStart()
    {

        Console.WriteLine("[Dev-Server] Server wurde gestartet!");
    }

    public override void OnStop()
    {
        Console.WriteLine("[Dev-Server] Server wurde gestoppt!");
    }
    
    public override IEntityFactory<IPlayer> GetPlayerFactory()
    {
        return new MyPlayerFactory();
    }
    
    public override IEntityFactory<IVehicle> GetVehicleFactory()
    {
        return new MyVehicleFactory();
    }
}