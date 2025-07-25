﻿using AltV.Net;
using Los_Angeles_Life_Server.Entities;
using Los_Angeles_Life_Server.Handlers;
using Newtonsoft.Json;

namespace Los_Angeles_Life_Server.Auth;

public class DiscordAuth : IScript
{
    [ClientEvent("DiscordToken")]
    public void DiscordToken(MyPlayer player, string token)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response;
        try
        {
            response = client.GetAsync("https://discordapp.com/api/users/@me").Result;
        }
        catch (AggregateException ex)
        {
            player.Kick("Authorization failed: " + ex);
            return;
        }

        if (!response.IsSuccessStatusCode)
        {
            player.Kick("Authorization failed");
            return;
        }

        var resultString = response.Content.ReadAsStringAsync().Result;
        dynamic result = JsonConvert.DeserializeObject(resultString) ?? throw new InvalidOperationException();

        if (result == null || result.id == null || result.username == null)
        {
            player.Kick("Authorization failed");
            return;
        }

        player.DiscordId = result.id;

        var loginHandler = new LoginHandler();
        loginHandler.PlayerAuth(player);
    }
}