using AltV.Net;
using AltV.Net.Enums;
using Los_Angeles_Life.Entities;

namespace Los_Angeles_Life.Handlers;

public class LoginHandler : IScript
{
    [ClientEvent("Client:Auth:Login")]
    public void OnPlayerLogin(MyPlayer player, int discordId)
    {
        if (DatabaseHandler.CheckAccountExists(discordId));
        {
            DatabaseHandler.LoadAccount(player);
            
            player.Model = (uint)PedModel.FreemodeMale01;
            player.Spawn(new AltV.Net.Data.Position(0, 0, 75), 0);
            player.IsLoggedIn = true;
            
            player.Emit("Client:Auth:CloseLoginHud");
        } 
    }

    [ClientEvent("Client:Auth:Register")]
    public void OnPlayerRegister(MyPlayer player, string playerName, int discordId)
    {
        Alt.Log("PlayerRegister");
        if (!DatabaseHandler.CheckAccountExists(discordId))
        {
            Alt.Log("PlayerRegister Check");
            DatabaseHandler.CreateAccount(playerName, discordId);
            
            player.Model = (uint)PedModel.FreemodeMale01;
            player.Spawn(new AltV.Net.Data.Position(0, 0, 75), 0);
            player.IsLoggedIn = true;
            
            player.Emit("Client:Auth:CloseLoginHud");
        }
    }
}