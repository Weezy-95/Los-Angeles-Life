using AltV.Net;
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
            player.Spawn(new AltV.Net.Data.Position(0, 0, 75), 0);
            player.IsLoggedIn = true;

            player.Emit("Client:Auth:CloseLoginHud");
        }
        else
        {
            player.PlayerName = player.Name;
            player.SocialClub = player.SocialClubId;
            DatabaseHandler.CreateAccount(player.PlayerName, player.DiscordId, player.SocialClub);

            player.Model = (uint)PedModel.FreemodeMale01;
            player.Spawn(new AltV.Net.Data.Position(0, 0, 75), 0);
            player.IsLoggedIn = true;

            player.Emit("Client:Auth:CloseLoginHud");
        }
    }

}