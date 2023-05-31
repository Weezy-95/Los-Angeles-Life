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
                Alt.Log("[Aduty] Admin: On");
                break;
            case "off":
                AdminHandler.NoDuty(player);
                Alt.Log("[Aduty] Admin: Off");
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
                IPlayer selectedPlayerToSetToSupporter = Alt.GetPlayersArray()[target].Value;
                MyPlayer playerToSetToSupporter = (MyPlayer)selectedPlayerToSetToSupporter;
                playerToSetToSupporter.AdminLevel = 1;
                DatabaseHandler.SetAdminLevel(playerToSetToSupporter.DiscordId, adminLevel);
                Alt.Log("[SetSupporter] " + playerToSetToSupporter.Name + " wurde auf Supporter gesetzt.");
            }
            catch (IndexOutOfRangeException ex)
            {
                Alt.Log("[SetSupporter] Keinen Spieler mit der ID " + target + " gefunden.");
                Alt.Log("[SetSupporter] SetDeveloperCmd: " + ex.Message);
            }
        }
        else
        {
            Alt.Log("[SetSupporter] Deine Berechtigung reicht nicht aus!");
        }
    }
    
    [Command("setDeveloper")]
    public static void SetDeveloperCmd(MyPlayer player, int adminLevel, ushort target)
    {
        if (!AdminHandler.CheckAdminPermissions(player, 3) && player.IsAduty)
        {
            try
            {
                IPlayer selectedPlayerToSetToDeveloper = Alt.GetPlayersArray()[target].Value;
                MyPlayer playerToSetToDeveloper = (MyPlayer)selectedPlayerToSetToDeveloper;
                playerToSetToDeveloper.AdminLevel = 3;
                DatabaseHandler.SetAdminLevel(playerToSetToDeveloper.DiscordId, adminLevel);
                Alt.Log("[SetDeveloper] " + playerToSetToDeveloper.Name + " wurde auf Developer gesetzt.");
            }
            catch (IndexOutOfRangeException ex)
            {
                Alt.Log("[SetDeveloper] Keinen Spieler mit der ID " + target + " gefunden.");
                Alt.Log("[SetDeveloper] SetDeveloperCmd: " + ex.Message);
            }
        }
        else
        {
            Alt.Log("[SetDeveloper] Deine Berechtigung reicht nicht aus!");
        }
    }
    
    [Command("setAdmin")]
    public static void SetAdminCmd(MyPlayer player, int adminLevel, ushort target)
    {
        if (!AdminHandler.CheckAdminPermissions(player, 4) && player.IsAduty)
        {
            try
            {
                IPlayer selectedPlayerToSetToAdmin = Alt.GetPlayersArray()[target].Value;
                MyPlayer playerToSetToAdmin = (MyPlayer)selectedPlayerToSetToAdmin;
                playerToSetToAdmin.AdminLevel = 3;
                DatabaseHandler.SetAdminLevel(playerToSetToAdmin.DiscordId, adminLevel);
                Alt.Log("[SetAdmin] " + playerToSetToAdmin.Name + " wurde auf Admin gesetzt.");
            }
            catch(IndexOutOfRangeException ex)
            {
                Alt.Log("[SetAdmin] Keinen Spieler mit der ID " + target + " gefunden.");
                Alt.Log("[SetAdmin] SetAdminCmd: " + ex.Message);
            }
        }
        else
        {
            Alt.Log("[SetAdmin] Deine Berechtigung reicht nicht aus!");
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
                Alt.Log("[Veh] Fahrzeug " + vehicleName + " wurde erfolgreich erstellt. Erstellt von " + player.PlayerName + ".");
            }
            
            player.SetIntoVehicle(vehicle, 1);
        }
        else
        {
            Alt.Log("[Veh] Du bist nicht im Admin Modus.");
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
                IPlayer selectedPlayerToRevive = Alt.GetPlayersArray()[target].Value;
                selectedPlayerToRevive.Spawn(new Position(selectedPlayerToRevive.Position.X, selectedPlayerToRevive.Position.Y, selectedPlayerToRevive.Position.Z), 0);
                selectedPlayerToRevive.Health = 200;
                Alt.Log("[Revive] " + selectedPlayerToRevive.Name + " wurde wiederbelebt.");
            }
            catch (IndexOutOfRangeException ex)
            {
                Alt.Log("[Revive] Keinen Spieler mit der ID " + target + " gefunden.");
                Alt.Log("[Revive] RevivePlayerCmd: " + ex.Message);
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
                IPlayer selectedPlayerToKill = Alt.GetPlayersArray()[target].Value;
                selectedPlayerToKill.Health = 0;
                Alt.Log("[Kill] " + selectedPlayerToKill.Name + " wurde getötet.");
            }
            catch (IndexOutOfRangeException ex)
            {
                Alt.Log("[Kill] Keinen Spieler mit der ID " + target + " gefunden.");
                Alt.Log("[Kill] KillPlayerCmd: " + ex.Message);
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