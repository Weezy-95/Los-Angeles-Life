import * as alt from 'alt-client';

function createGarageMarker(key, value) {
    if (key !== 'Client:Marker:Garage') return;

    const markerOptions = {
        color: new alt.RGBA(58, 38, 150, 255),
        type: 1
    };

    value.forEach(function(position) { 
        const marker = new alt.Utils.Marker(new alt.Vector3(position), markerOptions);
        marker.scale = new alt.Vector3(3.0);
    });
}

alt.on('globalSyncedMetaChange', (key, value) => {
    createGarageMarker(key, value);
});