/// <reference types="@altv/types-client" />
/// <reference types="@altv/types-natives" />

import alt from 'alt-client';
import native from 'natives';

alt.on('connectionComplete', () => {
    native.setScenarioTypeEnabled("WORLD_VEHICLE_POLICE_BIKE", false);
    native.setScenarioTypeEnabled("WORLD_VEHICLE_POLICE_CAR", false);
    native.setScenarioTypeEnabled("WORLD_VEHICLE_POLICE_NEXT_TO_CAR", false);
    native.setScenarioTypeEnabled("WORLD_VEHICLE_DRIVE_PASSENGERS", false);
    native.setScenarioTypeEnabled("WORLD_VEHICLE_SALTON_DIRT_BIKE", false);
    native.setScenarioTypeEnabled("WORLD_VEHICLE_BICYCLE_MOUNTAIN", false);
    native.setScenarioTypeEnabled("WORLD_VEHICLE_ATTRACTOR", false);
    native.setScenarioTypeEnabled("WORLD_VEHICLE_EMPTY", false);
    native.setScenarioTypeEnabled("WORLD_VEHICLE_BIKE_OFF_ROAD_RACE", false);
    native.setScenarioTypeEnabled("WORLD_VEHICLE_PARK_PARALLEL", false);
    native.setScenarioTypeEnabled("WORLD_VEHICLE_CONSTRUCTION_SOLO", false);
    native.setScenarioTypeEnabled("WORLD_VEHICLE_CONSTRUCTION_PASSENGERS", false);
    native.setScenarioTypeEnabled("WORLD_VEHICLE_TRUCK_LOGS", false);
    native.setScenarioTypeEnabled("DRIVE", false);
    native.startAudioScene("FBI_HEIST_H5_MUTE_AMBIENCE_SCENE"); // Used to stop police sound in town
    native.cancelAllPoliceReports(); // Used to stop default police radio around/In police vehicle
    native.clearAmbientZoneState("AZ_COUNTRYSIDE_PRISON_01_ANNOUNCER_GENERAL", false); // Turn off prison sound
    native.clearAmbientZoneState("AZ_COUNTRYSIDE_PRISON_01_ANNOUNCER_WARNING", false); // Turn off prison sound
    native.clearAmbientZoneState("AZ_COUNTRYSIDE_PRISON_01_ANNOUNCER_ALARM", false); // Turn off prison sound
    //native.setAmbientZoneState(0, 0, 0); // Set ambiant sound to 0,0,0
    native.clearAmbientZoneState("AZ_DISTANT_SASQUATCH", false);
    native.setAudioFlag("LoadMPData", true);
    native.setAudioFlag("DisableFlightMusic", true);
});
