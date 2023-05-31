import alt from 'alt-client';

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
