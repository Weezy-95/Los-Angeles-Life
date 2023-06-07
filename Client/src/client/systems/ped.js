import alt from 'alt-client';
import native from 'natives';


function createPed(type, hash, positionX, positionY, positionZ, rotation) {
    const modelHash = alt.hash(hash);
    alt.loadModel(modelHash);
    const ped = native.createPed(type, modelHash, positionX, positionY, positionZ, rotation, false, false);
    native.freezeEntityPosition(ped, true);
    native.setEntityInvincible(ped, true);
    native.setBlockingOfNonTemporaryEvents(ped, true);
    native.setEntityRotation(ped, 0, 0, rotation, 2, true);
    const test = native.getEntityRotation(ped, 2);
    alt.log(test);
}


alt.onServer('Client:Ped:Create', (type, hash, positionX, positionY, positionZ, rotation) => {
    createPed(type, hash, positionX, positionY, positionZ, rotation, false, false);
});