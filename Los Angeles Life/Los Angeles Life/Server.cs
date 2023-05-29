using AltV.Net;
using AltV.Net.Elements.Entities;
using Los_Angeles_Life.Factories;
using Los_Angeles_Life.Handlers;

namespace Los_Angeles_Life;

internal class Server : Resource
{
    private readonly DatabaseHandler _databaseHandler = new();

    private Timer _timer = null!;
    private const int Interval = 5000;

    public override void OnStart()
    {
        _databaseHandler.OpenConnection();
        _databaseHandler.LoadAllPlayers();
        _timer = new Timer(PositionHandler.HandlePositionSave, null, Interval, Interval);
    }

    public override void OnStop()
    {
        _databaseHandler.CloseConnection();
        _timer.Dispose();
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