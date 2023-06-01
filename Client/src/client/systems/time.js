/// <reference types="@altv/types-client" />
/// <reference types="@altv/types-natives" />

import alt from 'alt-client';
import native from 'natives';

alt.on('connectionComplete', () => {
    alt.setMsPerGameMinute(1000);
});