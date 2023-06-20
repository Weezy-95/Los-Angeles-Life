/// <reference types="@altv/types-client" />
/// <reference types="@altv/types-natives" />

import alt from 'alt-client';
import native from 'natives';

function createGarageMarker(storagePositionList) 
{
    const markerOptions = {
        color: new alt.RGBA(231, 114, 0, 255),
        type: 1
    };

    storagePositionList.forEach(function(markerPosition) 
    {
        const marker = new alt.Utils.Marker(markerPosition, markerOptions);
        marker.scale = new alt.Vector3(3.0);
    });
}

alt.onServer('Client:Marker:Garage', (storagePositionList) => {
    createGarageMarker(storagePositionList);
});