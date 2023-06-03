using AltV.Net;
using AltV.Net.Resources.Chat.Api;
using Los_Angeles_Life.Entities;
using System.Runtime.CompilerServices;

namespace Los_Angeles_Life.Handlers;

public class FactionCommandHandler : IScript
{
    [Command("reloadFactions")]
    public static void ReloadFactionsCmd(MyPlayer player)
    {
        if (FactionHandler.GetAllFactions())
        {
            Alt.Log("Factions erfolgreich geladen.");
        }
        else
        {
            Alt.Log("Factions konnten nicht geladen werden.");
        }
    }

    [Command("reloadFactionRanks")]
    public static void ReloadFactionRanksCmd(MyPlayer player)
    {
        if (FactionHandler.GetAllFactionRanks())
        {
            Alt.Log("FactionRanks erfolgreich geladen.");
        }
        else
        {
            Alt.Log("FactionRanks konnten nicht geladen werden.");
        }
    }

    [Command("reloadFactionSystem")]
    public static void ReloadFactionSystemCmd(MyPlayer player)
    {
        if(FactionHandler.GetAllFactions() && FactionHandler.GetAllFactionRanks())
        {
            Alt.Log("Faction System erfolgreich geladen.");
        }
        else
        {
            Alt.Log("Faction System konnte nicht geladen werden.");
        }
    }

    [Command("removeFactionRank")]
    public static void RemoveFactionRankCmd(MyPlayer player, string factionName, string factionRankName)
    {
        string replacedFactionName = ReplaceUnderscores(factionName);
        string replacedFactionRankName = ReplaceUnderscores(factionRankName);

        if(FactionHandler.RemoveFactionRank(replacedFactionName, replacedFactionRankName))
        {
            Alt.Log("Rank " + replacedFactionRankName + " wurde aus der Fraktion " + replacedFactionName + " enfernt.");
            FactionHandler.GetAllFactionRanks();
        }
        else
        {
            Alt.Log("Fehlerhafte Eingabe: " + factionName + " oder " + factionRankName + " existieren nicht.");
        }
    }

    [Command("removeAllFactionRanks")]
    public static void RemoveAllFactionRanksCmd(MyPlayer player, string factionName)
    {
        string replacedFactionName = ReplaceUnderscores(factionName);

        if (FactionHandler.RemoveAllFactionRanks(replacedFactionName))
        {
            Alt.Log("Es wurden alle FactionRanks von der Fraktion " + replacedFactionName + " entfernt.");
            FactionHandler.GetAllFactionRanks();
        }
        else
        {
            Alt.Log("Fehlerhafte Eingabe: " + factionName + " oder es wurden keine FactionRanks gefunden und entfernt.");
        }
    }

    [Command("addFactionRank")]
    public static void AddFactionRankCmd(MyPlayer player, string factionName, string factionRankName, int factionRankPermission)
    {
        string replacedFactionName = ReplaceUnderscores(factionName);
        string replacedFactionRankName = ReplaceUnderscores(factionRankName);

        if(FactionHandler.AddFactionRank(replacedFactionName, replacedFactionRankName, factionRankPermission))
        {
            Alt.Log(replacedFactionRankName + " wurde für die Fraktion " + replacedFactionName + " mit der Berechtigung " + factionRankPermission + "erstellt.");
            FactionHandler.GetAllFactionRanks();
        }
        else
        {
            Alt.Log("Fehlerhafte Eingabe: " + replacedFactionName + " existiert nicht.");
        }
    }

    [Command("updateFactionRankPermission")]
    public static void UpdateFactionRankPermissionCmd(MyPlayer player, string factionName, string factionRankName, int newFactionRankPermission)
    {
        string replacedFactionName = ReplaceUnderscores(factionName);
        string replacedFactionRankName = ReplaceUnderscores(factionRankName);

        if(FactionHandler.UpdateFactionRankPermission(replacedFactionName, replacedFactionRankName, newFactionRankPermission))
        {
            Alt.Log("Berechtigung: " + replacedFactionRankName + "[" + replacedFactionName + "] auf " + newFactionRankPermission + " geändert.");
            FactionHandler.GetAllFactionRanks();
        }
        else
        {
            Alt.Log("Fehlerhafte Eingabe: + " + replacedFactionName + " und/oder " + replacedFactionRankName + " existieren/existiert nicht.");
        }
    }

    [Command("updateFactionRankName")]
    public static void UpdateFactionRankNameCmd(MyPlayer player, string factionName, string factionRankName, string newFactionNamen)
    {
        string replacedFactionName = ReplaceUnderscores(factionName);
        string replacedFactionRankName = ReplaceUnderscores(factionRankName);
        string replacedNewFactionName = ReplaceUnderscores(newFactionNamen);

        if(FactionHandler.UpdateFactionRankName(replacedFactionName, replacedFactionRankName, replacedNewFactionName))
        {
            Alt.Log("Name: " + replacedFactionRankName + "[" + replacedFactionName + "] auf " + replacedNewFactionName + " geändert.");
            FactionHandler.GetAllFactionRanks();
        }
        else
        {
            Alt.Log("Fehlerhafte Eingabe: + " + replacedFactionName + " und/oder " + replacedFactionRankName + " existieren/existiert nicht.");
        }
    }

    private static string ReplaceUnderscores(string oldString)
    {
        string output = oldString.Replace("_", " ");
        return output;
    }
}