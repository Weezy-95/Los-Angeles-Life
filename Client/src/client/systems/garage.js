/// <reference types="@altv/types-client" />
/// <reference types="@altv/types-natives" />

import alt from 'alt-client';
import native from 'natives';

let garage;

alt.onServer('Client:Garage:Open', (name, fuel, plate) => {
    garage = new alt.WebView("http://resource/client/webview/garage/index.html");
    garage.emit('ShowGarage', name, fuel, plate)

    setTimeout(() => {
        if (garage) {
            garage.destroy();
            garage = null;
        }
    }, 5000);
});