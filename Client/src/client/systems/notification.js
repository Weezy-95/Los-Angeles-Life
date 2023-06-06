/// <reference types="@altv/types-client" />
/// <reference types="@altv/types-natives" />

import alt from 'alt-client';
import native from 'natives';

let notify;

alt.onServer('Client:ShowNotify', (message) => {
    notify = new alt.WebView("http://resource/client/webview/notifications/index.html");
    notify.emit('ShowNotify', message);

    setTimeout(() => {
        if (notify) {
            notify.destroy();
            notify = null;
        }
    }, 5000);
});
