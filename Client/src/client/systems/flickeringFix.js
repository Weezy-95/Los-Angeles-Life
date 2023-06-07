/// <reference types="@altv/types-client" />
/// <reference types="@altv/types-natives" />

import alt from 'alt-client';
import native from 'natives';

alt.everyTick(() => {
    native.drawRect(0, 0, 0, 0, 0, 0, 0, 0, 0);
});