using AltV.Net.Client;

namespace Los_Angeles_Life_Client.World;

public abstract class Sound : Client
{
    public static void Load()
    {
        Alt.OnConnectionComplete += () =>
        {
            Alt.Natives.SetScenarioTypeEnabled("WORLD_VEHICLE_POLICE_BIKE", false);
            Alt.Natives.SetScenarioTypeEnabled("WORLD_VEHICLE_POLICE_CAR", false);
            Alt.Natives.SetScenarioTypeEnabled("WORLD_VEHICLE_POLICE_NEXT_TO_CAR", false);
            Alt.Natives.SetScenarioTypeEnabled("WORLD_VEHICLE_DRIVE_PASSENGERS", false);
            Alt.Natives.SetScenarioTypeEnabled("WORLD_VEHICLE_SALTON_DIRT_BIKE", false);
            Alt.Natives.SetScenarioTypeEnabled("WORLD_VEHICLE_BICYCLE_MOUNTAIN", false);
            Alt.Natives.SetScenarioTypeEnabled("WORLD_VEHICLE_ATTRACTOR", false);
            Alt.Natives.SetScenarioTypeEnabled("WORLD_VEHICLE_EMPTY", false);
            Alt.Natives.SetScenarioTypeEnabled("WORLD_VEHICLE_BIKE_OFF_ROAD_RACE", false);
            Alt.Natives.SetScenarioTypeEnabled("WORLD_VEHICLE_PARK_PARALLEL", false);
            Alt.Natives.SetScenarioTypeEnabled("WORLD_VEHICLE_CONSTRUCTION_SOLO", false);
            Alt.Natives.SetScenarioTypeEnabled("WORLD_VEHICLE_CONSTRUCTION_PASSENGERS", false);
            Alt.Natives.SetScenarioTypeEnabled("WORLD_VEHICLE_TRUCK_LOGS", false);
            Alt.Natives.SetScenarioTypeEnabled("DRIVE", false);
            Alt.Natives.StartAudioScene("FBI_HEIST_H5_MUTE_AMBIENCE_SCENE");
            Alt.Natives.CancelAllPoliceReports();
            Alt.Natives.ClearAmbientZoneState("AZ_COUNTRYSIDE_PRISON_01_ANNOUNCER_GENERAL", false);
            Alt.Natives.ClearAmbientZoneState("AZ_COUNTRYSIDE_PRISON_01_ANNOUNCER_WARNING", false);
            Alt.Natives.ClearAmbientZoneState("AZ_COUNTRYSIDE_PRISON_01_ANNOUNCER_ALARM", false);
            Alt.Natives.ClearAmbientZoneState("AZ_DISTANT_SASQUATCH", false);
            Alt.Natives.SetAudioFlag("LoadMPData", true);
            Alt.Natives.SetAudioFlag("DisableFlightMusic", true);
        };
    }
}