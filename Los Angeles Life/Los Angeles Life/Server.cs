using AltV.Net;
using AltV.Net.Elements.Entities;
using Los_Angeles_Life.Factories;
using Los_Angeles_Life.Handlers;

namespace Los_Angeles_Life;

internal class Main : Resource
{
    private readonly DatabaseHandler _databaseHandler = new DatabaseHandler();
    
    public override void OnStart()
    {
        _databaseHandler.OpenConnection();
    }

    public override void OnStop()
    {
       _databaseHandler.CloseConnection();
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