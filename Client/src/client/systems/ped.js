import alt from 'alt-client';
import native from 'natives';


function createPed(type, hash, positionX, positionY, positionZ, rotation) {
    const modelHash = alt.hash(hash);
    alt.loadModel(modelHash);
    native.createPed(type, modelHash, positionX, positionY, positionZ, rotation, false, false);
}


alt.onServer('Client:Ped:Create', (type, hash, positionX, positionY, positionZ, rotation) => {
    createPed(type, hash, positionX, positionY, positionZ, rotation, false, false);
});