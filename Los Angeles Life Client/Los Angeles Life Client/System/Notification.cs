using AltV.Net.Client;
using AltV.Net.Client.Elements.Interfaces;
using System.Threading.Tasks;

namespace Los_Angeles_Life_Client.System
{
    public abstract class Notification : Client
    {
        private static IWebView? _notify;
        private static bool _isWebViewOpen;

        public static void Load()
        {
            Alt.OnServer("Client:ShowNotify", async (string message) =>
            {
                if (!_isWebViewOpen)
                {
                    _notify = Alt.CreateWebView("http://resource/net6.0/webview/notification/index.html");
                    _isWebViewOpen = true;
                }

                _notify?.Emit("ShowNotify", message);

                await Task.Delay(3000); 

                if (_notify != null && _isWebViewOpen)
                {
                    _notify.Destroy(); 
                    _isWebViewOpen = false;
                }
            });
        }
    }
}