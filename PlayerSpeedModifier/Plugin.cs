using BepInEx;
using BepInEx.Configuration;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;

namespace PlayerSpeedModifier;

[BepInPlugin(GUID, PluginName, PluginVersion)]
[BepInProcess("Digimon World Next Order.exe")]
public class Plugin : BasePlugin
{
    internal const string GUID = "BepInEx.YashaPlugins.DWNO.PlayerSpeedModifier";
    internal const string PluginName = "PlayerSpeedModifier";
    internal const string PluginVersion = "1.0";

    private static ConfigEntry<float> ConfigSpeedMultiplier { get; set; }

    public override void Load()
    {
        // ConfigBinding
        ConfigSpeedMultiplier = Config.Bind("General", "SpeedMultiplier", 1.5f, "Multiply the player's default speed.");

        // Plugin startup logic
        Log.LogInfo($"Plugin {GUID} is loaded!");
        Harmony.CreateAndPatchAll(typeof(Plugin));
    }

    [HarmonyPatch(typeof(CharacterMove), "Awake")]
    [HarmonyPostfix]
    private static void PatchPlayerSpeed(CharacterMove __instance)
    {
        __instance.m_defaultSpeed *= ConfigSpeedMultiplier.Value;
    }
}
