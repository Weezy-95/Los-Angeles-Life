using AltV.Net.Client;

namespace Los_Angeles_Life_Client;

public class Client : Resource
{
    public override void OnStart()
    {
        LoadingClasses.LoadClasses();
        Alt.LogInfo("Client Started!");
    }

    public override void OnStop()
    {
        Alt.LogInfo("Client Stopped!");
    }
}