/// <reference types="@altv/types-client" />

import alt from 'alt-client';

let loginHud;
const DISCORD_APP_ID = '1102181838484668476';

alt.onServer('connectionComplete', handleConnectionComplete);

async function handleConnectionComplete() {

    loginHud = new alt.WebView("http://resource/login/index.html");
    loginHud.focus();

    alt.showCursor(true);
    alt.toggleGameControls(false);
    alt.toggleVoiceControls(false);
}

async function getOAuthToken() {
    try {
        const token = await alt.Discord.requestOAuth2Token(DISCORD_APP_ID);
        alt.emitServer('token', token);
    } catch (e) {
        alt.logError("[Client] Es ist ein Fehler aufgetreten: " + e);
    }
}

getOAuthToken();