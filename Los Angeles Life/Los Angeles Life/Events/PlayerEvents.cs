using AltV.Net;
using AltV.Net.Elements.Entities;
using Los_Angeles_Life.Entities;
using Los_Angeles_Life.Handlers;
using Los_Angeles_Life.Handlers.Database;

namespace Los_Angeles_Life.Events
{
    public class PlayerEvents : IScript
    {
        private static readonly IVoiceChannel _channel = Alt.CreateVoiceChannel(true, 20f);

        [ScriptEvent(ScriptEventType.PlayerConnect)]
        public void OnPlayerConnect(MyPlayer player, string reason)
        {
            Alt.Log(DateTime.Now + ": " + $"Der Spieler {player.Name} mit der ID {player.Id} ist dem Server beigetreten!");
            
            player.PlayerDimension = -1;
            player.SetDateTime(DateTime.UtcNow);
            
            // Position for Login Camera
            player.Spawn(new AltV.Net.Data.Position((float)754.694, (float)1299.995, (float)360.294), 0);

            BlipManager.CreateFactionBlips(player);
            BlipManager.CreateGarageBlips(player);


            player.Emit("Client:Auth:Open");
            _channel.AddPlayer(player);
        }

        [ScriptEvent(ScriptEventType.PlayerDisconnect)]
        public void OnPlayerDisconnect(MyPlayer player, string reason)
        {
            Alt.Log(DateTime.Now + ": " + $"Der Spieler {player.Name} mit der ID {player.Id} hat den Server verlassen! Grund: " + reason);
            
            DatabaseHandler.SaveAccount(player);
            DatabaseHandler.SavePlayerPosition(player.DiscordId, player.PlayerPos, player.PlayerRot, player.PlayerDimension);

            if (player.IsInVehicle)
            {
                IVehicle vehicleToSave = player.Vehicle;
                VehicleHandler.SaveVehicle(vehicleToSave, player, player.Seat);
            }

            _channel.RemovePlayer(player);
        }

        [ScriptEvent(ScriptEventType.PlayerDead)]
        public void OnPlayerDead(MyPlayer player, IEntity killer, uint weapon)
        {
            Alt.Log(DateTime.Now + ": " + player.PlayerName +" wurde von " + killer + " mit " + weapon + " getötet!");
            
            DatabaseHandler.SavePlayerPosition(player.DiscordId, player.Position, player.Rotation, player.PlayerDimension);
        }

        [ScriptEvent(ScriptEventType.PlayerLeaveVehicle)]
        public void OnPlayerLeaveVehicle(IVehicle vehicle, IPlayer player, byte seat)
        {
            VehicleHandler.SaveVehicle(vehicle, player, seat);
        }
    }
}