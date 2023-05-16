/// <reference types="@altv/types-client" />
/// <reference types="@altv/types-natives" />

import alt from 'alt-client';
import native from 'natives';

let loginHud;
const DISCORD_APP_ID = '1102181838484668476';

alt.onServer('Client:Auth:Open', () => {
    loginHud = new alt.WebView("http://resource/client/webview/login/index.html");
    loginHud.on('AuthDiscord', getOAuthToken);
    loginHud.focus();

    alt.showCursor(true);
    alt.toggleGameControls(false);
    alt.toggleVoiceControls(false);
});

alt.onServer('Client:Auth:CloseLoginHud', () => {
   alt.showCursor(false);
   alt.toggleGameControls(true);
   alt.toggleVoiceControls(true);

   if (loginHud) {
       loginHud.destroy();
   }
});

async function getOAuthToken() {
    try {
        const token = await alt.Discord.requestOAuth2Token(DISCORD_APP_ID);
        alt.emitServer('DiscordToken', token);
    } catch (e) {
        alt.logError("[Client] Es gab einen Fehler mit dem Discord Token: " + e);
    }
}
