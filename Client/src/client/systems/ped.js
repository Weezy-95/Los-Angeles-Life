/// <reference types="@altv/types-client" />
/// <reference types="@altv/types-natives" />

import alt from 'alt-client';
import native from 'natives';


function createPed(pedtype, hash, posx, posy, posz, rot) {
    const modelHash = alt.hash(hash);
    alt.loadModel(modelHash);
    native.createPed(pedtype, modelHash, posx, posy, posz, rot, false, false);
}


alt.onServer('Client:Ped:Create', (pedtype, hash, posx, posy, posz, rot) => {
    createPed(pedtype, hash, posx, posy, posz, rot, false, false);
});

