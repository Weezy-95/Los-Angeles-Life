/// <reference types="@altv/types-client" />
/// <reference types="@altv/types-natives" />

import alt from 'alt-client';
import native from 'natives';

alt.on('globalSyncedMetaChange', (key, value, oldValue) => {
    if (key != 'ChangeWeather') return;
    native.setWeatherTypeOvertimePersist(value, 5);
});