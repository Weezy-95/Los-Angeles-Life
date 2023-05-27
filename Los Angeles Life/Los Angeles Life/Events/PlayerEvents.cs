using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using Los_Angeles_Life.Entities;
using Los_Angeles_Life.Handlers;
using Los_Angeles_Life.Model;

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
            player.Dimension = -1;
            
            // Position for Login Camera
            player.Spawn(new AltV.Net.Data.Position((float)754.694, (float)1299.995, (float)360.294), 0);

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

        [ScriptEvent(ScriptEventType.PlayerDead)]
        public void OnPlayerDead(MyPlayer player, IEntity killer, uint weapon)
        {
            Alt.Log(DateTime.Now + ": " + player.PlayerName +" wurde von " + killer + " mit " + weapon + " getötet!");
        }
    }
}