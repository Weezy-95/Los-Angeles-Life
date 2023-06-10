/// <reference types="@altv/types-client" />
/// <reference types="@altv/types-natives" />

import alt from 'alt-client';
import native from 'natives';


let garage;
let isWebViewOpen = false;
const interactionRadius = 2;
const pedFloatGarage = { x: 213.784, y: -808.47, z: 29.992 };

alt.on('keydown', (key) => {
    const playerPos = alt.Player.local.pos;
    const distance = playerPos.distanceTo(pedFloatGarage);

    if (distance <= interactionRadius) {
        if (key === "E".charCodeAt(0)) {
            if (!isWebViewOpen) {
                garage = new alt.WebView("http://resource/client/webview/garage/index.html");
                isWebViewOpen = true;
                garage.on('CloseGarageWebView', () => {
                    garage.destroy();
                    alt.showCursor(false);
                    alt.toggleGameControls(true);
                    garage = null;
                    isWebViewOpen = false;
                });
                garage.focus();
                alt.showCursor(true);
                alt.toggleGameControls(false);
            }
        }
    }
});


alt.onServer('Client:Garage:Open', (name, fuel, plate) => {
    if (isWebViewOpen && garage) {
        garage.emit('ShowGarage', name, fuel, plate);
    }
});

// Für den Backend aufruf des Events
alt.onServer('Client:Garage:CloseWebView', () => {
    if (isWebViewOpen && garage) {
        alt.showCursor(false);
        alt.toggleGameControls(true);
        garage.destroy();
        garage = null;
        isWebViewOpen = false;
    }
});
