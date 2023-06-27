using AltV.Net.Client;
using AltV.Net.Client.Elements.Data;
using AltV.Net.Client.Elements.Interfaces;
using AltV.Net.Data;

namespace Los_Angeles_Life_Client.System;

public abstract class Garage : Client
{
    private static bool _isWebViewOpen;
    private const int InteractionsRadius = 2;
    private static readonly Position PedFloatGarage = new((float)213.784, (float)-808.47, (float)29.992);
    private static IWebView? _garageHud;
    
    public static void Load()
    {
        Alt.OnKeyDown += (key) =>
        {
            var playerPos = Alt.LocalPlayer.Position;
            var distance = playerPos.Distance(PedFloatGarage);

            if (distance <= InteractionsRadius)
            {
                if (key == Key.E)
                {
                    //Alt.EmitServer("Client:Garage:SendPlayerInformation", 1000);
                    if (!_isWebViewOpen)
                    {
                        _garageHud = Alt.CreateWebView("http://resource/net6.0/webview/garage/index.html");
                        _isWebViewOpen = true;
                        _garageHud.On("CloseGarageWebView", () =>
                        {
                            _garageHud.Destroy();
                            Alt.ShowCursor(false);
                            Alt.GameControlsEnabled = true;
                            _garageHud = null;
                            _isWebViewOpen = false;
                        });

                        _garageHud.On("ParkIntoGarage", () =>
                        {
                            Alt.EmitServer("Client:Garage:ParkIntoGarage", 1000);
                        });
                        
                        _garageHud.Focus();
                        Alt.ShowCursor(true);
                        Alt.GameControlsEnabled = false;
                    }
                }
            }
        };
        
        Alt.OnServer("Client:Garage:Open", (string name, float fuel, string plate) =>
        {
            if (_garageHud != null && _isWebViewOpen)
            {
                _garageHud.Emit("ShowGarage", name, fuel, plate);
            }
        });
        
        Alt.OnServer("Client:Garage:CloseWebView", () =>
        {
            if (_garageHud != null && _isWebViewOpen )
            {
                Alt.ShowCursor(false);
                Alt.GameControlsEnabled = true;
                _garageHud.Destroy();
                _garageHud = null;
                _isWebViewOpen = false;
            }
        });
    }
}