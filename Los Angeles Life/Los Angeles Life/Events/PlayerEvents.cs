using AltV.Net;
using AltV.Net.Elements.Entities;
using Los_Angeles_Life.Entities;

namespace Los_Angeles_Life.Events;

public class PlayerEvents : IScript
{
    [ScriptEvent(ScriptEventType.PlayerConnect)]
    public void OnPlayerConnect(MyPlayer player, string reason)
    {
        Alt.Log(DateTime.Now + ": "+ $"Der Spieler {player.Name} mit der ID {player.Id} ist dem Server beigetreten!");
        player.SetDateTime(DateTime.Now);
        player.Emit("Client:Auth:Open");
    }
}