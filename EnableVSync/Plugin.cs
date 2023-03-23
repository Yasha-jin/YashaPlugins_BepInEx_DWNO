using BepInEx;
using BepInEx.Configuration;
using BepInEx.Unity.IL2CPP;
using UnityEngine;

namespace EnableVSync;

[BepInPlugin(GUID, PluginName, PluginVersion)]
[BepInProcess("Digimon World Next Order.exe")]
public class Plugin : BasePlugin
{
    internal const string GUID = "Yashajin.DWNO.EnableVSync";
    internal const string PluginName = "EnableVSync";
    internal const string PluginVersion = "1.0";

    public override void Load()
    {
        // Plugin startup logic
        Log.LogInfo($"Plugin {GUID} is loaded!");

        QualitySettings.vSyncCount = 1;
    }
}
