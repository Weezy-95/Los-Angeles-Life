using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Enums;
using Los_Angeles_Life.Entities;

namespace Los_Angeles_Life.Handlers;

public class LoginHandler : IScript
{
    public void PlayerAuth(MyPlayer player)
    {
        if (DatabaseHandler.CheckAccountExists(player.DiscordId))
        {
            DatabaseHandler.LoadAccount(player);

            player.Model = (uint)PedModel.FreemodeMale01;
            player.Spawn(player.PlayerPos, 0);
            player.Dimension = 0;

            player.Emit("Client:Auth:CloseLoginHud");
        }
        else
        {
            player.PlayerName = player.Name;
            player.SocialClub = player.SocialClubId;
            player.Money = 1500;
            player.AdminLevel = 0;
            player.IsWhitelisted = false;
            Position playerPos = new Position(0f, 0f, 75f);
            DatabaseHandler.CreateAccount(player.PlayerName, player.DiscordId, player.SocialClub, player.AdminLevel, player.Money, player.IsWhitelisted, playerPos);
            player.Spawn(playerPos, 0);
            player.Dimension = 0;

            player.Model = (uint)PedModel.FreemodeMale01;
         
            player.Emit("Client:Auth:CloseLoginHud");
        }
    }

}