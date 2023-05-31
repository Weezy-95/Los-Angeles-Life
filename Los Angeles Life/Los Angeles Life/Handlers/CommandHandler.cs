using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using AltV.Net.Resources.Chat.Api;
using Los_Angeles_Life.Entities;

namespace Los_Angeles_Life.Handlers;

public class CommandHandler : IScript
{
    [Command("aduty")]
    public static void AdutyCmd(MyPlayer player, string status)
    {
        if (!AdminHandler.CheckAdminPermissions(player, 1)) return;
        switch (status)
        {
            case "on":
                AdminHandler.Aduty(player);
                player.Emit("Client:Notification", "Du bist im Admin Modus!");

                break;
            case "off":
                AdminHandler.NoDuty(player);
                player.Emit("Client:Notification", "Du hast den Admin Modus beendet!");
                break;
        }
    }

    [Command("setSupporter")]
    public static void SetSupporterCmd(MyPlayer player, int adminLevel, int target)
    {
        if (AdminHandler.CheckAdminPermissions(player, 2))
        {
            if (player.IsAduty)
            {
                try
                {
                    IPlayer selectedAdminPlayer = Alt.GetPlayersArray()[target].Value;
                    MyPlayer? setPlayerAdmin = (MyPlayer)selectedAdminPlayer;
                    
                    setPlayerAdmin.AdminLevel = 1;
                    DatabaseHandler.SetAdminLevel(selectedAdminPlayer.DiscordId, adminLevel);
                }
                catch (Exception ex)
                {
                    Alt.Log("Fehler beim setzen des Admin Status: " + ex);
                }
            }
            else
            {
                Alt.Log("Du bist nicht im Adminmodus");
            }
        }
        else
        {
            Alt.Log("Deine Berechtigung reicht nicht aus!");
        }
    }
    
    [Command("setDeveloper")]
    public static void SetDeveloperCmd(MyPlayer player, int adminLevel, ushort target)
    {
        if (!AdminHandler.CheckAdminPermissions(player, 3) && player.IsAduty)
        {
            foreach (IPlayer playerTarget in Alt.GetAllPlayers())
            {
                if (playerTarget.Id.Equals(target))
                {
                    player = (MyPlayer)playerTarget;
                    player.AdminLevel = 2;
                    DatabaseHandler.SetAdminLevel(player.DiscordId, adminLevel);
                }
                else
                {
                    Alt.Log("Ungültige Spieler ID");
                }
            }
        }
        else
        {
            Alt.Log("Deine Berechtigung reicht nicht aus!");
        }
    }
    
    [Command("setAdmin")]
    public static void SetAdminCmd(MyPlayer player, int adminLevel, ushort target)
    {
        if (!AdminHandler.CheckAdminPermissions(player, 4) && player.IsAduty)
        {
            foreach (IPlayer playerTarget in Alt.GetAllPlayers())
            {
                if (playerTarget.Id.Equals(target))
                {
                    player = (MyPlayer)playerTarget;
                    player.AdminLevel = 3;
                    DatabaseHandler.SetAdminLevel(player.DiscordId, adminLevel);
                }
                else
                {
                    Alt.Log("Ungültige Spieler ID");
                }
            }
        }
        else
        {
            Alt.Log("Deine Berechtigung reicht nicht aus!");
        }
    }
    
    [Command("veh")]
    public static void CreateVehicleCmd(MyPlayer player, string vehicleName)
    {
        if (player.IsAduty)
        {
            IVehicle vehicle = Alt.CreateVehicle(Alt.Hash(vehicleName), new Position(player.Position.X, player.Position.Y +1.5f, player.Position.Z),  player.Rotation);
            if (vehicle != null)
            {
                player.SendChatMessage("Fahrzeug " + vehicleName + " wurde erfolgreich erstellt. Erstellt von " + player.PlayerName + ".");
            }
            
            player.SetIntoVehicle(vehicle, 1);
        }
        else
        {
            Alt.Log("Du bist nicht im Admin Modus");
        }
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
            Alt.Log("Kein Spieler mit der ID " + target + " gefunden. Grund: " + ex);
        }   
    }

    [Command("kick")]
    public static void KickPlayerFromServerCmd(MyPlayer player)
    {
        // TODO 1: richtige Kick Funktion einbauen
        player.Kick("Selbst gekickt!");
    }
    
    
    [Command("dim")]
    public static void ShowDim(MyPlayer player)
    {
        Alt.Log("Dim: " + player.PlayerDim);
    }
}