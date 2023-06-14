using AltV.Net.Client;
using AltV.Net.Client.Elements.Interfaces;

namespace Los_Angeles_Life_Client.System;

public abstract class Hud : Client
{
    private static IWebView? _hud;
    private static bool _isWebViewOpen;
    
    public static void Load()
    {
        Alt.OnServer("Client:Hud:OpenWebView", () =>
        {
            _hud = Alt.CreateWebView("http://resource/net6.0/webview/hud/index.html");
            _hud.Emit("ShowHud");
        });
        
        Alt.OnServer("Client:Hud:CloseWebView", () =>
        {
            if (_hud != null && _isWebViewOpen )
            {
                Alt.ShowCursor(false);
                Alt.GameControlsEnabled = true;
                _hud.Destroy();
                _hud = null;
                _isWebViewOpen = false;
            }
        });
    }
}