/// <reference types="@altv/types-client" />

import alt from 'alt-client';

let notify;

alt.onServer('Client:Notification', () => {
    notify = new alt.WebView("http://resource/client/webview/notifications/index.html");
});
