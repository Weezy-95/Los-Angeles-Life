using AltV.Net.Client;
using AltV.Net.Client.Elements.Interfaces;

namespace Los_Angeles_Life_Client.System;

public abstract class DiscordAuth : Client
{
    private const string DiscordAppId = "1102181838484668476";
    private static IWebView? _loginHud;

    public static void Load()
    {
        Alt.OnServer("Client:Auth:Open", () =>
        {
            _loginHud = Alt.CreateWebView("http://resource/net6.0/webview/login/index.html");
            _loginHud.On("AuthDiscord", GetOAuthToken);
            _loginHud.Focus();

            Alt.ShowCursor(true);
            Alt.GameControlsEnabled = false;
            Alt.VoiceControlsEnabled = false;
            Alt.Natives.DisplayRadar(false);

            var cam = Alt.Natives.CreateCamWithParams("DEFAULT_SCRIPTED_CAMERA", (float)-1373.9209, (float)-1116.0791,
                (float)21.29309, 0, 0, 130, 90, true, 0);
            Alt.Natives.SetCamActive(cam, true);
            Alt.Natives.RenderScriptCams(true, false, 0, true, false, 0);
            Alt.Natives.FreezeEntityPosition(Alt.LocalPlayer.ScriptId, true);
        });

        Alt.OnServer("Client:Auth:CloseLoginHud", () =>
        {
            Alt.ShowCursor(false);
            Alt.GameControlsEnabled = true;
            Alt.VoiceControlsEnabled = true;
            Alt.BeginScaleformMovieMethodMinimap("SETUP_HEALTH_ARMOUR");
            Alt.Natives.DisplayRadar(true);
            Alt.Natives.ScaleformMovieMethodAddParamInt(3);
            Alt.Natives.EndScaleformMovieMethod();
            Alt.Natives.FreezeEntityPosition(Alt.LocalPlayer.ScriptId, false);
            Alt.Natives.DestroyAllCams(true);
            Alt.Natives.RenderScriptCams(false, false, 0, false, false, 0);

            _loginHud?.Destroy();
        });
        
        async void GetOAuthToken()
        {
            try
            {
                var token = await Alt.Discord.RequestOAuth2Token(DiscordAppId);
                Alt.EmitServer("DiscordToken", token);
            }
            catch (Exception ex)
            {
                Alt.LogInfo("Fehler mit der Discord Auth: " + ex);
                throw;
            }
        }
    }
}