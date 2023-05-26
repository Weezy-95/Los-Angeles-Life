using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using Los_Angeles_Life.Entities;

namespace Los_Angeles_Life.Handlers;

public class CommandHandler : IScript
{
    [Command("veh")]
    public static void veh_CMD(MyPlayer player, string vehicleName)
    {
        IVehicle vehicle = Alt.CreateVehicle(Alt.Hash(vehicleName), new Position(player.Position.X, player.Position.Y +1.5f, player.Position.Z),  player.Rotation);
        if (vehicle != null)
            player.SendChatMessage("You just Created a " + vehicleName);
        player.SetIntoVehicle(vehicle, 1);
    }
    
    [Command("pos")]
    public static void pos_CMD(MyPlayer player)
    {
        Alt.Log(player.Position + player.Rotation.ToString());
    }

    [Command("revive")]
    public static void revive_CMD(MyPlayer player, ushort target)
    {
        foreach (var playerTarget in Alt.GetAllPlayers())
        {
            if (playerTarget.Id.Equals(target))
            {
                player = (MyPlayer)playerTarget;
                player.Spawn(new Position(player.Position.X, player.Position.Y, player.Position.Z), 0);
                player.Health = 200;
            }
            else
            {
                Alt.Log("Kein Spieler zum Reviven gefunden");
            }
        }
    }

    [Command("kill")]
    public static void test_CMD(MyPlayer player, ushort target)
    {
        foreach (var playerTarget in Alt.GetAllPlayers())
        {
            if (playerTarget.Id.Equals(target))
            {
                player = (MyPlayer)playerTarget;
                player.Health = 0;
            }
            else
            {
                Alt.Log("Kein Spieler zum Töten gefunden");
            }
        }
    }
}