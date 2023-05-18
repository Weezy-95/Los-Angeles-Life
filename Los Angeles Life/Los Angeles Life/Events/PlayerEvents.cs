using AltV.Net;
using AltV.Net.Elements.Entities;
using Los_Angeles_Life.Entities;
using Los_Angeles_Life.Handlers;

namespace Los_Angeles_Life.Events
{
    public class PlayerEvents : IScript
    {
        private static readonly IVoiceChannel _channel = Alt.CreateVoiceChannel(true, 20f);

        [ScriptEvent(ScriptEventType.PlayerConnect)]
        public void OnPlayerConnect(MyPlayer player, string reason)
        {
            Alt.Log(DateTime.Now + ": " + $"Der Spieler {player.Name} mit der ID {player.Id} ist dem Server beigetreten!");
            player.SetDateTime(DateTime.Now);
            player.Emit("Client:Auth:Open");
            _channel.AddPlayer(player);
        }

        [ScriptEvent(ScriptEventType.PlayerDisconnect)]
        public void OnPlayerDisconnect(MyPlayer player, string reason)
        {
            Alt.Log(DateTime.Now + ": " + $"Der Spieler {player.Name} mit der ID {player.Id} hat den Server verlassen! Grund: " + reason);
            DatabaseHandler.SaveAccount(player);
            _channel.RemovePlayer(player);
        }
    }
}