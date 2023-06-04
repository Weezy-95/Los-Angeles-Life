/// <reference types="@altv/types-client" />
/// <reference types="@altv/types-natives" />

import alt from 'alt-client';
import native from 'natives';

alt.onServer('Client:SendFactionList', (PosX, PosY, PosZ, BlipId, BlipColorId, Name) => {
    alt.log("Test");
    createBlip(PosX, PosY, PosZ, BlipId, BlipColorId, Name);
});

function createBlip(posX, posY, posZ, blipId, blipColorId, name) {
    const blip = new alt.PointBlip(posX, posY, posZ);
    blip.sprite = blipId;
    blip.color = blipColorId;
    blip.name = name;
    blip.display = 3;
}
