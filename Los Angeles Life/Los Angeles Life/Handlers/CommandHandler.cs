using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using AltV.Net.Resources.Chat.Api;
using Los_Angeles_Life.Entities;

namespace Los_Angeles_Life.Handlers;

public class CommandHandler : IScript
{
    [Command("veh")]
    public static void CreateVehicleCmd(MyPlayer player, string vehicleName)
    {
        IVehicle vehicle = Alt.CreateVehicle(Alt.Hash(vehicleName), new Position(player.Position.X, player.Position.Y +1.5f, player.Position.Z),  player.Rotation);
        if (vehicle != null)
        {
            player.SendChatMessage("Fahrzeug " + vehicleName + " wurde erfolgreich erstellt. Erstellt von " + player.PlayerName + ".");
        }
            
        player.SetIntoVehicle(vehicle, 1);
    }
    
    [Command("pos")]
    public static void CurrentPlayerPositionCmd(MyPlayer player)
    {
        Alt.Log(player.Position + player.Rotation.ToString());
    }

    [Command("revive")]
    public static void RevivePlayerCmd(MyPlayer player, ushort target)
    {
        foreach (IPlayer playerTarget in Alt.GetAllPlayers())
        {
            if (playerTarget.Id.Equals(target))
            {
                playerTarget.Spawn(new Position(playerTarget.Position.X, playerTarget.Position.Y, playerTarget.Position.Z), 0);
                playerTarget.Health = 200;
            }
            else
            {
                Alt.Log("Kein Spieler zum Reviven gefunden");
            }
        }
    }

    [Command("kill")]
    public static void KillPlayerCmd(MyPlayer player, ushort target)
    {
        foreach (IPlayer playerTarget in Alt.GetAllPlayers())
        {
            if (playerTarget.Id.Equals(target))
            {
                playerTarget.Health = 0;
            }
            else
            {
                Alt.Log("Kein Spieler zum Töten gefunden");
            }
        }
    }


    // Beispiel -> /weapon WelcheWaffe WieVielMunition WelcherSpieler -> /weapon 584646201 90 0
    [Command("weapon")]
    public static void GivePlayerWeaponCmd(MyPlayer player, uint weapon, int ammo, ushort target)
    {
        try
        {
            if(!Enum.IsDefined(typeof(WeaponModel), weapon))
            {
                Alt.Log(weapon + " ist keine gültige Waffe.");
                return;
            }

            IPlayer selectedPlayer = Alt.GetPlayersArray()[target].Value;
            selectedPlayer.GiveWeapon(weapon, ammo, true);
            Alt.Log(player.PlayerName + "(" + player.PlayerId + ")" + " vergab die Waffe " + weapon + " an " + selectedPlayer.Name + "(" + target + ")" + " mit " + ammo + " Munition.");
        }
        catch(Exception ex)
        {
            Alt.Log("Kein Spieler mit der ID " + target + " gefunden.");
        }   
    }

    [Command("kick")]
    public static void KickPlayerFromServerCmd(MyPlayer player)
    {
        // richtige Kick Funktion einbauen
        player.Kick("Selbst gekickt!");
    }

    [Command("save")]
    public static void SaveTestCmd(MyPlayer player)
    {
        DatabaseHandler.TestSaveRot(player.DiscordId, player.PlayerRot);
    }    
}