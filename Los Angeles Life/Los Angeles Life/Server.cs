using AltV.Net;
using AltV.Net.Elements.Entities;
using Los_Angeles_Life.Factories;
using Los_Angeles_Life.Garages;
using Los_Angeles_Life.Handlers;
using Los_Angeles_Life.Vehicles;

namespace Los_Angeles_Life;

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