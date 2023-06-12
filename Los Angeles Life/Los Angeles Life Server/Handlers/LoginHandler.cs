using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Enums;
using Los_Angeles_Life_Server.Entities;
using Los_Angeles_Life_Server.Handlers.Database;

namespace Los_Angeles_Life_Server.Handlers;

public class LoginHandler : IScript
{
    private Timer _timer = null!;
    private const int Interval = 5000;
    
    public void PlayerAuth(MyPlayer player)
    {
        if (DatabaseHandler.CheckAccountExists(player.DiscordId))
        {
            DatabaseHandler.LoadAccount(player);
            player.Model = (uint)PedModel.FreemodeMale01;
            player.PlayerPos = new Position(player.PlayerPos.X, player.PlayerPos.Y, player.PlayerPos.Z);
            player.Spawn(player.PlayerPos, 1);
            PedHandler.LoadPeds(player);

            player.Emit("Client:Auth:CloseLoginHud");
            
            _timer = new Timer(PlayerSaveHandler.SaveAllPlayersPositions, null, Interval, Interval);
        }
        else
        {
            player.PlayerName = player.Name;
            player.SocialClub = player.SocialClubId;
            player.Cash = 1500f;
            player.Bank = 0f;
            player.AdminLevel = 0;
            player.IsWhitelisted = false;
            player.Model = (uint)PedModel.FreemodeMale01;
            player.PlayerDimension = 0;

            Position playerPos = new Position(0f, 0f, 75f);
            Rotation playerRot = new Rotation(0f, 0f, 0f);
            DatabaseHandler.CreateAccount(player.PlayerName, player.DiscordId, player.SocialClub, player.AdminLevel, player.IsWhitelisted, player.Cash, player.Bank, playerPos, playerRot, player.PlayerDimension);
            player.Spawn(playerPos, 1);
            PedHandler.LoadPeds(player);
         
            player.Emit("Client:Auth:CloseLoginHud");
            
            _timer = new Timer(PlayerSaveHandler.SaveAllPlayersPositions, null, Interval, Interval);
        }
    }
}