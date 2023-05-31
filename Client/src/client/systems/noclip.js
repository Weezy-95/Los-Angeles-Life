/// <reference types="@altv/types-client" />
/// <reference types="@altv/types-natives" />

import alt from 'alt-client';
import native from 'natives';

let noclipEnabled = false;
const NoclipSpeed = 5.0;

alt.onServer("Client:noclip:start", () => {
    noclipEnabled = true;
});

alt.onServer("Client:noclip:stop", () => {
    noclipEnabled = false;
});

alt.on("keydown", (key) => {
    if (noclipEnabled) {
        if (key === "w") { // Vorwärts
            const forwardVector = new alt.Vector3(0, 1, 0);
            const direction = forwardVector.mul(NoclipSpeed);
            alt.emitServer("noclip:move", direction);
        } else if (key === "s") { // Rückwärts
            const forwardVector = new alt.Vector3(0, -1, 0);
            const direction = forwardVector.mul(NoclipSpeed);
            alt.emitServer("noclip:move", direction);
        } else if (key === "a") { // Links
            const rightVector = new alt.Vector3(-1, 0, 0);
            const direction = rightVector.mul(NoclipSpeed);
            alt.emitServer("noclip:move", direction);
        } else if (key === "d") { // Rechts
            const rightVector = new alt.Vector3(1, 0, 0);
            const direction = rightVector.mul(NoclipSpeed);
            alt.emitServer("noclip:move", direction);
        } else if (key === "space") { // Hoch
            const upVector = new alt.Vector3(0, 0, 1);
            const direction = upVector.mul(NoclipSpeed);
            alt.emitServer("noclip:move", direction);
        } else if (key === "shift") { // Runter
            const upVector = new alt.Vector3(0, 0, -1);
            const direction = upVector.mul(NoclipSpeed);
            alt.emitServer("noclip:move", direction);
        }
    }
});

alt.onServer("noclip:move", (direction) => {
    const player = alt.Player.local;
    player.pos = player.pos.add(direction);
});


