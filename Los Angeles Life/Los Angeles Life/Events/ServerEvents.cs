using AltV.Net;

namespace Los_Angeles_Life.Events
{
    abstract class ServerEvents : IScript
    {
        [ScriptEvent(ScriptEventType.ServerStarted)]
        public void OnServerStarted()
        {
            Alt.Log("Server erfolgreich gestartet");
        }
    }
}
