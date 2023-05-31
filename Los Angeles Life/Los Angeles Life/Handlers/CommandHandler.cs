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
                player.Emit("Client:ShowNotify", "Du bist im Admin Modus!");
                break;
            case "off":
                AdminHandler.NoDuty(player);
                player.Emit("Client:ShowNotify", "Du hast den Admin Modus beendet!");
                break;
        }
    }

    [Command("setSupporter")]
    public static void SetSupporterCmd(MyPlayer player, int adminLevel, ushort target)
    {
        if (!AdminHandler.CheckAdminPermissions(player, 2) && player.IsAduty)
        {
            try
            {
                try
                {
                    IPlayer selectedAdminPlayer = Alt.GetPlayersArray()[target].Value;
                    MyPlayer? setPlayerAdmin = (MyPlayer)selectedAdminPlayer;
                    
                    setPlayerAdmin.AdminLevel = 1;
                    DatabaseHandler.SetAdminLevel(selectedAdminPlayer.DiscordId, adminLevel);
                    player.Emit("Client:ShowNotify", " Du hast " + selectedAdminPlayer.Name + " zum Supporter gemacht");
                }
                catch (Exception ex)
                {
                    Alt.Log("Fehler beim setzten des Admin Status!" + ex);
                    player.Emit("Client:ShowNotify", "Die Spieler ID gibt es nicht!");
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                player.Emit("Client:ShowNotify", "Du bist nicht im Admin Modus!");
            }
        }
        else
        {
            player.Emit("Client:ShowNotify", "Deine Berechtigung reicht nicht aus!");        
        }
    }
    
    [Command("setDeveloper")]
    public static void SetDeveloperCmd(MyPlayer player, int adminLevel, ushort target)
    {
        if (AdminHandler.CheckAdminPermissions(player, 3))
        {
            if (player.IsAduty)
            {
                try
                {
                    IPlayer selectedAdminPlayer = Alt.GetPlayersArray()[target].Value;
                    MyPlayer? setPlayerAdmin = (MyPlayer)selectedAdminPlayer;
                    
                    setPlayerAdmin.AdminLevel = 2;
                    DatabaseHandler.SetAdminLevel(selectedAdminPlayer.DiscordId, adminLevel);
                    player.Emit("Client:ShowNotify", " Du hast " + selectedAdminPlayer.Name + " zum Developer gemacht");
                }
                catch (Exception ex)
                {
                    Alt.Log("Fehler beim setzten des Admin Status!" + ex);
                    player.Emit("Client:ShowNotify", "Die Spieler ID gibt es nicht!");
                }
            }
            else
            {
                player.Emit("Client:ShowNotify", "Du bist nicht im Admin Modus!");
            }
        }
        else
        {
            player.Emit("Client:ShowNotify", "Deine Berechtigung reicht nicht aus!");        
        }
    }
    
    [Command("setAdmin")]
    public static void SetAdminCmd(MyPlayer player, int adminLevel, ushort target)
    {
        if (AdminHandler.CheckAdminPermissions(player, 4))
        {
            if (player.IsAduty)
            {
                try
                {
                    IPlayer selectedAdminPlayer = Alt.GetPlayersArray()[target].Value;
                    MyPlayer? setPlayerAdmin = (MyPlayer)selectedAdminPlayer;
                    
                    setPlayerAdmin.AdminLevel = 3;
                    DatabaseHandler.SetAdminLevel(selectedAdminPlayer.DiscordId, adminLevel);
                    player.Emit("Client:ShowNotify", " Du hast " + selectedAdminPlayer.Name + " zum Admin gemacht");
                }
                catch (Exception ex)
                {
                    Alt.Log("Fehler beim setzten des Admin Status!" + ex);
                    player.Emit("Client:ShowNotify", "Die Spieler ID gibt es nicht!");
                }
            }
            else
            {
                player.Emit("Client:ShowNotify", "Du bist nicht im Admin Modus!");
            }
        }
        else
        {
            player.Emit("Client:ShowNotify", "Deine Berechtigung reicht nicht aus!");        
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
                player.Emit("Client:ShowNotify", "Du hast einen " + vehicleName + " gespawnt!");
            }
            
            player.SetIntoVehicle(vehicle, 1);
        }
        else
        {
            player.Emit("Client:ShowNotify", "Du bist nicht im Admin Modus!");
        }
    }
    
    [Command("pos")]
    public static void CurrentPlayerPositionCmd(MyPlayer player)
    {
        if (player.IsAduty)
        {
            Alt.Log("[Pos] " + player.Position.ToString());
        }
        else
        {
            Alt.Log("[Pos] Du bist nicht im Admin Modus.");
        }
            
    }

    [Command("revive")]
    public static void RevivePlayerCmd(MyPlayer player, ushort target)
    {
        if (player.IsAduty)
        {
            try
            {
                playerTarget.Spawn(new Position(playerTarget.Position.X, playerTarget.Position.Y, playerTarget.Position.Z), 0);
                playerTarget.Health = 200;
                player.Emit("Client:ShowNotify", "Du hast " + playerTarget.Name + " wiederbelebt!");
            }
            catch (IndexOutOfRangeException ex)
            {
                player.Emit("Client:ShowNotify", "Keinen Spieler mit der ID gefunden!");
            }
        }
        else
        {
            Alt.Log("[Revive] Du bist nicht im Admin Modus.");
        }  
    }

    [Command("kill")]
    public static void KillPlayerCmd(MyPlayer player, ushort target)
    {
        if (player.IsAduty)
        {
            try
            {
                playerTarget.Health = 0;
                player.Emit("Client:ShowNotify", "Du hast " + playerTarget.Name + " getötet!");
            }
            catch (IndexOutOfRangeException ex)
            {
                player.Emit("Client:ShowNotify", "Keinen Spieler mit der ID gefunden!");
            }
        }
        else
        {
            Alt.Log("[Kill] Du bist nicht im Admin Modus.");
        }
    }

    
    [Command("weapon")]
    public static void GivePlayerWeaponCmd(MyPlayer player, string weapon, int ammo, ushort target)
    {
        if (player.IsAduty)
        {
            try
            {
                if (!Enum.IsDefined(typeof(WeaponModel), weapon))
                {
                    Alt.Log(weapon + " ist keine gültige Waffe.");
                    return;
                }

                IPlayer selectedPlayer = Alt.GetPlayersArray()[target].Value;
                string selectedWeapon = "weapon_" + weapon;

                selectedPlayer.GiveWeapon(Alt.Hash(selectedWeapon), ammo, true);
                Alt.Log("[Weapon] " + player.PlayerName + "(" + player.PlayerId + ")" + " vergab die Waffe " + weapon + " an " + selectedPlayer.Name + "(" + target + ")" + " mit " + ammo + " Munition.");
            }
            catch (Exception ex)
            {
                Alt.Log("[Weapon] Keinen Spieler mit der ID " + target + " gefunden.");
                Alt.Log("[Weapon] GivePlayerWeaponCmd: " + ex.Message);
            }
        }
        else
        {
            Alt.Log("[Weapon] Du bist nicht im Admin Modus.");
        }
               
    }

    [Command("kick")]
    public static void KickPlayerFromServerCmd(MyPlayer player, int target, string reason)
    {
        if (player.IsAduty)
        {
            try
            {
                IPlayer selectedPlayerToKick = Alt.GetPlayersArray()[target].Value;
                Alt.Log("[Kick] " + selectedPlayerToKick.Name + " wurde gekickt. Grund: " + reason);

                selectedPlayerToKick.Kick(reason);
            }
            catch (IndexOutOfRangeException ex)
            {
                Alt.Log("[Kick] Keinen Spieler mit der ID " + target + " gefunden.");
                Alt.Log("[Kick] KickPlayerFromServerCmd: " + ex.Message);
            }
        }
        else
        {
            Alt.Log("[Kick] Du bist nicht im Admin Modus.");
        }
            
    }
}