using AltV.Net;
using AltV.Net.Elements.Entities;
using Los_Angeles_Life.Factories;
using Los_Angeles_Life.Handlers;

namespace Los_Angeles_Life;

internal class Main : Resource
{
    public override void OnStart()
    {
        DatabaseHandler databaseHandler = new DatabaseHandler();
        Console.WriteLine("[DEV] Server started!");
    }

    public override void OnStop()
    {
        Console.WriteLine("[DEV] Server stopped!");
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