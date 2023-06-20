/// <reference types="@altv/types-client" />
/// <reference types="@altv/types-natives" />

import alt from 'alt-client';
import native from 'natives';

let loginHud;
let cam;
const DISCORD_APP_ID = '1102181838484668476';

alt.onServer('Client:Auth:Open', () => {
    loginHud = new alt.WebView("http://resource/client/webview/login/index.html");
    loginHud.on('AuthDiscord', getOAuthToken);
    loginHud.focus();

    alt.showCursor(true);
    alt.toggleGameControls(false);
    alt.toggleVoiceControls(false);
    native.displayRadar(false);

    cam = native.createCamWithParams('DEFAULT_SCRIPTED_CAMERA', -1373.9209, -1116.0791, 21.29309, 0, 0,  130, 90, true, 0);
    native.setCamActive(cam, true);
    native.renderScriptCams(true, false, 0, true, false, 0);
    native.freezeEntityPosition(alt.Player.local.scriptID, true);
});

alt.onServer('Client:Auth:CloseLoginHud', () => {
    alt.showCursor(false);
    alt.toggleGameControls(true);
    alt.toggleVoiceControls(true);
    alt.beginScaleformMovieMethodMinimap('SETUP_HEALTH_ARMOUR');
    native.displayRadar(true);
    native.scaleformMovieMethodAddParamInt(3);
    native.endScaleformMovieMethod();
    native.freezeEntityPosition(alt.Player.local.scriptID, false);
    native.destroyAllCams(true);
    native.renderScriptCams(false, false, 0, false, false, 0);

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


