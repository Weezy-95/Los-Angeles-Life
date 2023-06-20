/// <reference types="@altv/types-client" />
/// <reference types="@altv/types-natives" />

import alt from 'alt-client';
import native from 'natives';

let notify;

alt.onServer('Client:ShowNotify', (message) => {
    if (!notify) {
        notify = new alt.WebView("http://resource/client/webview/notifications/index.html");
    }
    notify.emit('ShowNotify', message);
    KillWebview().then(() => {
        if (notify) {
            notify.destroy();
            notify = null;
        }
    });
});

async function KillWebview() {
    await new Promise(resolve => setTimeout(resolve, 5000));
}
