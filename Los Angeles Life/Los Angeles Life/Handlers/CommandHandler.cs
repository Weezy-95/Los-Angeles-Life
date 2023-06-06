using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using AltV.Net.Resources.Chat.Api;
using Los_Angeles_Life.Entities;
using Los_Angeles_Life.Handlers.Database;
using Los_Angeles_Life.Vehicles;

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
        if (!AdminHandler.CheckAdminPermissions(player, 4))
        {
            if (player.IsAduty)
            {
                try
                {
                    IPlayer selectedPlayerToSetToSupporter = Alt.GetPlayersArray()[target].Value;
                    MyPlayer playerToSetToSupporter = (MyPlayer)selectedPlayerToSetToSupporter;
                    playerToSetToSupporter.AdminLevel = 1;
                    DatabaseHandler.SetAdminLevel(playerToSetToSupporter.DiscordId, adminLevel);
                    player.Emit("Client:ShowNotify", " Du hast " + playerToSetToSupporter.Name + " zum Supporter gemacht");
                }
                catch (IndexOutOfRangeException ex)
                {
                    Alt.Log("Fehler beim setzten des Supporter Status!");
                    player.Emit("Client:ShowNotify", "Die Spieler ID gibt es nicht!");
                    throw;
                }
            }
            else
            {
                player.Emit("Client:ShowNotify", "Du bist nicht im Admin Modus!");
            }
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
                    DatabaseHandler.SetAdminLevel(setPlayerAdmin.DiscordId, adminLevel);
                    player.Emit("Client:ShowNotify", " Du hast " + setPlayerAdmin.Name + " zum Developer gemacht");
                }
                catch (IndexOutOfRangeException ex)
                {
                    Alt.Log("Fehler beim setzten des Developer Status!");
                    player.Emit("Client:ShowNotify", "Die Spieler ID gibt es nicht!");
                    throw;
                }
            }
            else
            {
                player.Emit("Client:ShowNotify", "Du bist nicht im Admin Modus!");
            }
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
                    DatabaseHandler.SetAdminLevel(setPlayerAdmin.DiscordId, adminLevel);
                    player.Emit("Client:ShowNotify", " Du hast " + setPlayerAdmin.Name + " zum Admin gemacht");
                }
                catch (IndexOutOfRangeException ex)
                {
                    Alt.Log("Fehler beim setzten des Admin Status!");
                    player.Emit("Client:ShowNotify", "Die Spieler ID gibt es nicht!");
                    throw;
                }
            }
            else
            {
                player.Emit("Client:ShowNotify", "Du bist nicht im Admin Modus!");
            }
        }
    }

    // Fliegt in der Alpha raus
    [Command("veh")]
    public static void CreateVehicleCmd(MyPlayer player, string vehicleName)
    {
        if (player.IsAduty)
        {
            if (!Enum.IsDefined(typeof(VehicleModel), vehicleName))
            {
                Alt.Log("Falscher Fahrzeugname: " + vehicleName);
                return;
            }

            IVehicle vehicle = Alt.CreateVehicle(Alt.Hash(vehicleName), new Position(player.Position.X, player.Position.Y + 1.5f, player.Position.Z), player.Rotation);
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
        if (AdminHandler.CheckAdminPermissions(player, 4))
        {
            if (player.IsAduty)
            {
                Alt.Log("[Pos] " + player.Position + " [Rot] " + player.Rotation);
            }
            else
            {
                player.Emit("Client:ShowNotify", "Du bist nicht im Admin Modus!");
            }
        }
    }

    [Command("revive")]
    public static void RevivePlayerCmd(MyPlayer player, ushort target)
    {
        if (AdminHandler.CheckAdminPermissions(player, 4))
        {
            if (player.IsAduty)
            {
                try
                {
                    IPlayer selectedPlayerToRevive = Alt.GetPlayersArray()[target].Value;
                    selectedPlayerToRevive.Spawn(new Position(selectedPlayerToRevive.Position.X, selectedPlayerToRevive.Position.Y, selectedPlayerToRevive.Position.Z), 0);
                    selectedPlayerToRevive.Health = 200;

                    player.Emit("Client:ShowNotify", "Du hast " + selectedPlayerToRevive.Name + " wiederbelebt!");
                }
                catch (IndexOutOfRangeException ex)
                {
                    Alt.Log("[Command] revive Fehler");
                    player.Emit("Client:ShowNotify", "Keinen Spieler mit der ID gefunden!");
                    throw;
                }
            }
            else
            {
                player.Emit("Client:ShowNotify", "Du bist nicht im Admin Modus!");
            }
        }
    }

    [Command("kill")]
    public static void KillPlayerCmd(MyPlayer player, ushort target)
    {
        if (AdminHandler.CheckAdminPermissions(player, 4))
        {
            if (player.IsAduty)
            {
                try
                {
                    IPlayer selectedPlayerToKill = Alt.GetPlayersArray()[target].Value;
                    selectedPlayerToKill.Health = 0;

                    player.Emit("Client:ShowNotify", "Du hast " + selectedPlayerToKill.Name + " getötet!");
                }
                catch (IndexOutOfRangeException ex)
                {
                    Alt.Log("[Command] kill Fehler");
                    player.Emit("Client:ShowNotify", "Keinen Spieler mit der ID gefunden!");
                    throw;
                }
            }
            else
            {
                player.Emit("Client:ShowNotify", "Du bist nicht im Admin Modus!");

            }
        }
    }


    [Command("weapon")]
    public static void GivePlayerWeaponCmd(MyPlayer player, string weapon, int ammo, ushort target)
    {
        if (AdminHandler.CheckAdminPermissions(player, 4))
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
                catch (IndexOutOfRangeException ex)
                {
                    player.Emit("Client:ShowNotify", "Keinen Spieler mit der ID " + target + " gefunden!");
                    Alt.Log("[Command] GivePlayerWeaponCmd Fehler");
                    throw;
                }
            }
            else
            {
                player.Emit("Client:ShowNotify", "Du bist nicht im Admin Modus!");
            }
        }
    }

    [Command("kick")]
    public static void KickPlayerFromServerCmd(MyPlayer player, int target, string reason)
    {
        if (AdminHandler.CheckAdminPermissions(player, 4))
        {
            if (player.IsAduty)
            {
                try
                {
                    IPlayer selectedPlayerToKick = Alt.GetPlayersArray()[target].Value;
                    player.Emit("Client:ShowNotify", selectedPlayerToKick.Name + " wurde gekickt. Grund: " + reason);

                    selectedPlayerToKick.Kick(reason);
                }
                catch (IndexOutOfRangeException ex)
                {
                    player.Emit("Client:ShowNotify", "Keinen Spieler mit der ID " + target + " gefunden!");
                    Alt.Log("[Command] KickPlayerFromServer Fehler");
                    throw;
                }
            }
            else
            {
                Alt.Log("[Kick] Du bist nicht im Admin Modus.");
            }
        }
    }

    [Command("freeze")]
    public static void FreezePlayerCmd(MyPlayer player, int target)
    {
        if (AdminHandler.CheckAdminPermissions(player, 4))
        {
            if (player.IsAduty)
            {
                try
                {
                    IPlayer selectedPlayerFreeze = Alt.GetPlayersArray()[target].Value;
                    selectedPlayerFreeze.Frozen = !selectedPlayerFreeze.Frozen;
                }
                catch (IndexOutOfRangeException ex)
                {
                    player.Emit("Client:ShowNotify", "Keinen Spieler mit der ID " + target + " gefunden!");
                    Alt.Log("[Command] Freeze Fehler");
                    throw;
                }
            }
            else
            {
                player.Emit("Client:ShowNotify", "Du bist nicht im Admin Modus!");
            }
        }
    }

    [Command("goto")]
    public static void GoToCmd(MyPlayer player, float posX, float posY, float posZ)
    {
        if (AdminHandler.CheckAdminPermissions(player, 4))
        {
            if (player.IsAduty)
            {
                player.Position = new Position(posX, posY + 1f, posZ);
                player.Emit("Client:ShowNotify", "Du hast dich zu " + posX + " ," + posY + " ," + posZ + " teleportiert!");
            }
            else
            {
                player.Emit("Client:ShowNotify", "Du bist nicht im Admin Modus!");
            }
        }
    }

    [Command("tpto")]
    public static void TpToCmd(MyPlayer player, int target)
    {
        if (AdminHandler.CheckAdminPermissions(player, 4))
        {
            if (player.IsAduty)
            {
                try
                {
                    IPlayer selectedPlayerPort = Alt.GetPlayersArray()[target].Value;
                    player.Position = new Position(selectedPlayerPort.Position.X, selectedPlayerPort.Position.Y, selectedPlayerPort.Position.Z);
                }
                catch (IndexOutOfRangeException ex)
                {
                    player.Emit("Client:ShowNotify", "Keinen Spieler mit der ID " + target + " gefunden!");
                    Alt.Log("[Command] TpTo: " + ex.Message);
                    throw;
                }
            }
            else
            {
                player.Emit("Client:ShowNotify", "Du bist nicht im Admin Modus!");
            }
        }
    }

    [Command("testveh")]
    public static void TestVehCmd(MyPlayer player, int vehicleTemplateId)
    {
        VehicleHandler.CreateNewVehicle(vehicleTemplateId, "PO PO 1337", player.Position, player.Rotation, player);
    }
}