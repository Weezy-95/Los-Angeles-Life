using AltV.Net;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using Los_Angeles_Life.Entities;

namespace Los_Angeles_Life.Events;

public class PlayerEvents : IScript
{
    [ScriptEvent(ScriptEventType.PlayerConnect)]
    public void OnPlayerConnect(MyPlayer player, string reason)
    {
        Alt.Log($"Der Spieler {player.Name} mit der ID {player.Id} ist dem Server beigetreten");

        player.Model = (uint)PedModel.FreemodeMale01;
        player.SetDateTime(DateTime.Now);
        player.Spawn(new AltV.Net.Data.Position(0, 0, 75), 0);
    }
}