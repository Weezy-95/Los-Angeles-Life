/// <reference types="@altv/types-client" />
/// <reference types="@altv/types-natives" />

import alt from 'alt-client';
import native from 'natives';

function createGarageMarker(storagePositionList) {
    //if (key !== 'Client:Marker:Garage') return;

    const markerOptions = {
        color: new alt.RGBA(58, 38, 150, 255),
        type: 1
    };

    storagePositionList.forEach(function(position) {
        const marker = new alt.Utils.Marker(new alt.Vector3(position), markerOptions);
        marker.scale = new alt.Vector3(3.0);
    });
}

alt.onServer('Client:Marker:Garage', (storagePositionList) => {
    alt.log("Test");
    createGarageMarker(storagePositionList);
});

/*alt.on('globalSyncedMetaChange', (key, value) => {
    createGarageMarker(key, value);
});*/