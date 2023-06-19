using AltV.Net;
using AltV.Net.Elements.Entities;
using Los_Angeles_Life_Server.Factories;
using Los_Angeles_Life_Server.Handlers;

namespace Los_Angeles_Life_Server;

internal class Server : Resource
{
    private readonly DatabaseHandler _databaseHandler = new();

    public override void OnStart()
    {
        _databaseHandler.LoadPlayerCount();
        WeatherHandler.StartWeather();
        FactionHandler.LoadFactionsAndFactionRanks();
        VehicleHandler.LoadVehicleSystem();
        GarageHandler.LoadGarageSystem();
        PedHandler.LoadPedSystem();
        ColShapeHandler.LoadingColShapeEventSystem();
        ColShapeHandler.CreateGarageStorageColShapesAndMarker();
    }

    public override void OnStop()
    {
        DatabaseHandler.CloseConnection();
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