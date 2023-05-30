using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Enums;
using Los_Angeles_Life.Entities;

namespace Los_Angeles_Life.Handlers;

public class LoginHandler : IScript
{
    private Timer _timer = null!;
    private const int Interval = 5000;
    
    public void PlayerAuth(MyPlayer player)
    {
        if (DatabaseHandler.CheckAccountExists(player.DiscordId))
        {
            DatabaseHandler.LoadAccount(player);
            Alt.Log("Auth: " + player.PlayerPos + "Dim: " + player.PlayerDim);
            player.Model = (uint)PedModel.FreemodeMale01;
            player.Position = new Position(player.PlayerPos.X, player.PlayerPos.Y, player.PlayerPos.Z);

            player.Emit("Client:Auth:CloseLoginHud");
            
            _timer = new Timer(PlayerSaveHandler.SaveAllPlayersPositions, null, Interval, Interval);
        }
        else
        {
            player.PlayerName = player.Name;
            player.SocialClub = player.SocialClubId;
            player.Money = 1500;
            player.AdminLevel = 0;
            player.IsWhitelisted = false;
            player.PlayerDim = 0;
            player.Model = (uint)PedModel.FreemodeMale01;

            Position playerPos = new Position(0f, 0f, 75f);
            Rotation playerRot = new Rotation(0f, 0f, 0f);
            DatabaseHandler.CreateAccount(player.PlayerName, player.DiscordId, player.SocialClub, player.AdminLevel, player.Money, player.IsWhitelisted, playerPos, playerRot, player.PlayerDim);
            player.Spawn(playerPos, 0);
         
            player.Emit("Client:Auth:CloseLoginHud");
            
            _timer = new Timer(PlayerSaveHandler.SaveAllPlayersPositions, null, Interval, Interval);
        }
    }

}