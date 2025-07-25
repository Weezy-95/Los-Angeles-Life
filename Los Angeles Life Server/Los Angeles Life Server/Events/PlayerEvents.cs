﻿using AltV.Net;
using AltV.Net.Elements.Entities;
using Los_Angeles_Life_Server.Entities;
using Los_Angeles_Life_Server.Handlers;
using Los_Angeles_Life_Server.Handlers.Database;

namespace Los_Angeles_Life_Server.Events
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
            player.Spawn(new AltV.Net.Data.Position((float)-1374.7781, (float) -1124.9802, (float)4.493), 0);

            BlipManager.CreateFactionBlips(player);
            BlipManager.CreateGarageBlips(player);
            ColShapeHandler.LoadingColShapeEventSystem();
            ColShapeHandler.CreateGarageStorageColShapesAndMarker(player);

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

        [ScriptEvent(ScriptEventType.PlayerRemove)]
        public void OnPlayerRemove(MyPlayer player)
        {
            Alt.Log("PlayerRemoved");
        }

        [ScriptEvent(ScriptEventType.PlayerDead)]
        public void OnPlayerDead(MyPlayer player, IEntity killer, uint weapon)
        {
            Alt.Log(DateTime.Now + ": " + player.PlayerName +" wurde von " + killer + " mit " + weapon + " getötet!");
            
            DatabaseHandler.SavePlayerPosition(player.DiscordId, player.Position, player.Rotation, player.PlayerDimension);
            player.Emit("Client:Hud:CloseWebView");
        }

        [ScriptEvent(ScriptEventType.PlayerLeaveVehicle)]
        public void OnPlayerLeaveVehicle(IVehicle vehicle, MyPlayer player, byte seat)
        {
            VehicleHandler.SaveVehicle(vehicle, player, seat);
        }
    }
}