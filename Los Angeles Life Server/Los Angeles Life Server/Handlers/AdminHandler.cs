using AltV.Net;
using AltV.Net.Enums;
using Los_Angeles_Life_Server.Entities;

namespace Los_Angeles_Life_Server.Handlers;

public abstract class AdminHandler : IScript
{
    public static bool CheckAdminPermissions(MyPlayer player, int requiredLevel)
    {
        return player.AdminLevel >= requiredLevel;
    }
    
    public static void Aduty(MyPlayer player)
    {
        player.IsAduty = true;
        player.Model = (uint)PedModel.MovAlien01;
        player.Invincible = true;
    }

    public static void NoDuty(MyPlayer player)
    {
        player.IsAduty = false;
        player.Model = (uint)PedModel.FreemodeMale01;
        player.Invincible = false;
    }
}