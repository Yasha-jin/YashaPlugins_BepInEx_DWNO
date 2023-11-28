using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using static PlayerData;
using static StorageData;

namespace LoadAllSaves;

[BepInPlugin(GUID, PluginName, PluginVersion)]
[BepInProcess("Digimon World Next Order.exe")]
public class Plugin : BasePlugin
{
    internal const string GUID = "LoadAllSaves";
    internal const string PluginName = "LoadAllSaves";
    internal const string PluginVersion = "1.0.0";

    public override void Load()
    {
        // Plugin startup logic
        Log.LogInfo($"Plugin {GUID} is loaded!");
        Harmony.CreateAndPatchAll(typeof(Plugin));
    }

    [HarmonyPatch(typeof(CSaveDataHeader), "ReadSaveData")]
    [HarmonyPostfix]
    public static void ReadSaveData_Postfix(CSaveDataHeader __instance)
    {
        __instance.m_isMatchStaemId = true;
    }
}
