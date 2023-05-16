using AltV.Net;
using Los_Angeles_Life.Entities;
using Newtonsoft.Json;

namespace Los_Angeles_Life.Auth;

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
            player.Kick("Authorization failed");
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

        // Example of returned properties
        Alt.Log($"Id: {result.id}");
        Alt.Log($"Name: {result.username}#{result.discriminator}");
    }
}