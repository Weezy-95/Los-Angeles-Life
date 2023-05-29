using AltV.Net;
using AltV.Net.Elements.Entities;
using Los_Angeles_Life.Factories;
using Los_Angeles_Life.Handlers;

namespace Los_Angeles_Life;

internal class Server : Resource
{
    private readonly DatabaseHandler _databaseHandler = new DatabaseHandler();

    private Timer timer;
    private int interval = 5000; // Intervall in Millisekunden (hier: 1 Sekunde)

    public override void OnStart()
    {
        _databaseHandler.OpenConnection();
        _databaseHandler.LoadAllPlayers();
        timer = new Timer(PositionHandler.HandlePositionSave, null, interval, interval);
    }

    public override void OnStop()
    {
        _databaseHandler.CloseConnection();
        timer.Dispose();
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