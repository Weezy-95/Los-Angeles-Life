/// <reference types="@altv/types-client" />
/// <reference types="@altv/types-natives" />

import alt from 'alt-client';
import native from 'natives';

alt.on('globalSyncedMetaChange', (key, value) => {
    changeWeather(key, value);
});

alt.on('connectionComplete', () => {
    let value = '';
    changeWeather('ChangeWeather', value);
});

function changeWeather(key, value) 
{
    if (key !== 'ChangeWeather') return;

    native.setWeatherTypeOvertimePersist(value, 5);
}