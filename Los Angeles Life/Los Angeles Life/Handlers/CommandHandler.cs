using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using Los_Angeles_Life.Entities;

namespace Los_Angeles_Life.Handlers;

public class CommandHandler : IScript
{
    [CommandEvent(CommandEventType.CommandNotFound)]
    public void OnCommondNotFound(MyPlayer player, string command)
    {
        player.SendChatMessage("{FF0000} " + command + " nicht gefunden!");
    }
    
    [Command("veh")]
    public static void veh_CMD(MyPlayer player, string vehicleName)
    {
        IVehicle vehicle = Alt.CreateVehicle(Alt.Hash(vehicleName), new Position(player.Position.X, player.Position.Y +1.5f, player.Position.Z),  player.Rotation);
        if (vehicle != null) { player.SendChatMessage("You just Created a " + vehicleName); }
        player.SetIntoVehicle(vehicle, 1);
    }
    
    [Command("pos")]
    public static void pos_CMD(MyPlayer player)
    {
        Alt.Log(player.Position + player.Rotation.ToString());
    }
}