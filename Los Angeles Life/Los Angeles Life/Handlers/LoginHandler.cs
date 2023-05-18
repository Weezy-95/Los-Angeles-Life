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
            Alt.Log("Lädt Account mit der Discord Id: " + player.DiscordId + " und der Player Id: " + player.PlayerId);
            
            player.Model = (uint)PedModel.FreemodeMale01;
            player.Spawn(new AltV.Net.Data.Position(0, 0, 75), 0);
            player.IsLoggedIn = true;
            
            player.Emit("Client:Auth:CloseLoginHud");
        }
        else
        {
            DatabaseHandler.CreateAccount(player.PlayerName, player.DiscordId);
            Alt.Log("Erstellt den Account mit der Discord Id: " + player.DiscordId);
            
            player.Model = (uint)PedModel.FreemodeMale01;
            player.Spawn(new AltV.Net.Data.Position(0, 0, 75), 0);
            player.IsLoggedIn = true;
        
            player.Emit("Client:Auth:CloseLoginHud");
        }
    }
}