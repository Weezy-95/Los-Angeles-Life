/// <reference types="@altv/types-client" />
/// <reference types="@altv/types-natives" />

import alt from 'alt-client';
import native from 'natives';

alt.on('connectionComplete', () => {
    let date = new Date(Date.now());

    native.setClockTime(date.getUTCHours(), date.getUTCMinutes(), date.getUTCSeconds());
    alt.setMsPerGameMinute(1000);
});