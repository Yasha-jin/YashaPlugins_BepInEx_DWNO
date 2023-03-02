using BepInEx;
using BepInEx.Configuration;
using BepInEx.Unity.IL2CPP;
using UnityEngine;

namespace EnableVSync;

[BepInPlugin(GUID, PluginName, PluginVersion)]
[BepInProcess("Digimon World Next Order.exe")]
public class Plugin : BasePlugin
{
    internal const string GUID = "BepInEx.YashaPlugins.DWNO.EnableVSync";
    internal const string PluginName = "EnableVSync";
    internal const string PluginVersion = "1.0";

    private static ConfigEntry<VSyncList> ConfigVSync { get; set; }

    private enum VSyncList
    {
        On = 1,
        Off = 0
    }

    public override void Load()
    {
        // ConfigBinding
        ConfigVSync = Config.Bind("General", "VSync", VSyncList.On);

        // Plugin startup logic
        Log.LogInfo($"Plugin {GUID} is loaded!");

        QualitySettings.vSyncCount = (int)ConfigVSync.Value;
    }
}
