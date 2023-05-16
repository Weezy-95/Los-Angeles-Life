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
        alt.emitServer('DiscordToken', token);
    } catch (e) {
        alt.logError("[Client] Es gab einen Fehler mit dem Discord Token: " + e);
    }
}

getOAuthToken();