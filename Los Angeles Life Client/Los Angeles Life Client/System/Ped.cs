using AltV.Net.Client;

namespace Los_Angeles_Life_Client.System;

public abstract class Ped : Client
{
    public static void Load()
    {
        Alt.OnServer("Client:Ped:Create", (int type, string hash, float positionX, float positionY, float positionZ, float rotation, bool isNewPed) =>
        {
            CreatePed(type, hash, positionX, positionY, positionZ, rotation, isNewPed);
        });
    }

    private static void CreatePed(int type, string hash, float positionX, float positionY, float positionZ, float rotation, bool isNewPed)
    {
        if(!isNewPed) return;

        var modelHash = Alt.Hash(hash);
        Alt.LoadModel(modelHash);
        var ped = Alt.Natives.CreatePed(type, modelHash, positionX, positionY, positionZ, rotation, false, false);
        Alt.Natives.FreezeEntityPosition(ped, true);
        Alt.Natives.SetEntityInvincible(ped, true);
        Alt.Natives.SetBlockingOfNonTemporaryEvents(ped, true);
        Alt.Natives.SetEntityRotation(ped, 0, 0, rotation, 2, true);
    }
}