using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;

namespace HideEvoMon;

[BepInPlugin(GUID, PluginName, PluginVersion)]
[BepInProcess("Digimon World Next Order.exe")]
public class Plugin : BasePlugin
{
    internal const string GUID = "BepInEx.YashaPlugins.DWNO.HideEvoMon";
    internal const string PluginName = "HideEvoMon";
    internal const string PluginVersion = "1.0";

    public override void Load()
    {
        // Plugin startup logic
        Log.LogInfo($"Plugin {GUID} is loaded!");
        Harmony.CreateAndPatchAll(typeof(Plugin));
    }

    [HarmonyPatch(typeof(uGenelogyInformationUI), "Initialize")]
    [HarmonyPostfix]
    private static void PatchMonVisibilityInfo(uGenelogyInformationUI __instance)
    {
        var HeadTransform = __instance.m_After.transform.Find("Head").gameObject.transform;
        HeadTransform.Find("Nature").gameObject.active = false;
        HeadTransform.Find("Property").gameObject.active = false;
        HeadTransform.Find("Digimon").gameObject.active = false;
    }
}
