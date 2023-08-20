using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using System;
using static PlayerData;
using Il2CppInterop.Runtime.InteropTypes.Arrays;

namespace UncappedStats;

[BepInPlugin(GUID, PluginName, PluginVersion)]
[BepInProcess("Digimon World Next Order.exe")]
public class Plugin : BasePlugin
{
    internal const string GUID = "Yashajin.DWNO.UncappedStats";
    internal const string PluginName = "UncappedStats";
    internal const string PluginVersion = "1.0.0";

    public override void Load()
    {
        // Plugin startup logic
        Log.LogInfo($"Plugin {GUID} is loaded!");
        Harmony.CreateAndPatchAll(typeof(Plugin));
    }

    [HarmonyPatch(typeof(uResultPanelDigimonBase), "SetRiseData")]
    [HarmonyPostfix]
    public static void SetRiseData_Postfix(int hpRiseValue,
        int mpRiseValue,
        int forcefulnessRiseValue,
        int robustnessRiseValue,
        int clevernessRiseValue,
        int rapidityRiseValue,
        int fatigueRiseValue,
        uResultPanelDigimonBase __instance)
    {
        __instance.m_riseValues[0] = hpRiseValue;
        __instance.m_riseValues[1] = mpRiseValue;
        __instance.m_riseValues[2] = forcefulnessRiseValue;
        __instance.m_riseValues[3] = robustnessRiseValue;
        __instance.m_riseValues[4] = clevernessRiseValue;
        __instance.m_riseValues[5] = rapidityRiseValue;
        __instance.m_riseValues[6] = fatigueRiseValue;

        for (int i = 0; i < __instance.m_riseTexts.Length; i++)
        {
            __instance.m_riseTexts[i].text = "+" + __instance.m_riseValues[i].ToString();
        }
    }

    [HarmonyPatch(typeof(DigimonCtrl), "CommonParameterUp", new Type[] { typeof(Il2CppStructArray<int>) })]
    [HarmonyPrefix]
    public static bool CommonParameterUp_Prefix(Il2CppStructArray<int> up, DigimonCtrl __instance)
    {
        DigimonCommonData commonData = __instance.m_data.m_commonData;
        commonData.m_hpMax += up[0];
        commonData.m_mpMax += up[1];
        commonData.m_forcefulness += up[2];
        commonData.m_robustness += up[3];
        commonData.m_cleverness += up[4];
        commonData.m_rapidity += up[5];
        return false;
    }

    [HarmonyPatch(typeof(PartnerData), "SetDiathesisHp")]
    [HarmonyPrefix]
    public static bool SetDiathesisHp_Prefix(int _value, PartnerData __instance)
    {
        __instance.m_diathesisHp = _value;
        return false;
    }

    [HarmonyPatch(typeof(DigimonCommonData), "SetHpMax")]
    [HarmonyPrefix]
    public static bool SetHpMax_Prefix(int value, DigimonCommonData __instance)
    {
        __instance.m_hpMax = value;
        return false;
    }

    [HarmonyPatch(typeof(DigimonCommonData), "AddHpMax")]
    [HarmonyPrefix]
    public static bool AddHpMax_Prefix(int value, DigimonCommonData __instance)
    {
        __instance.m_hpMax += value;
        return false;
    }

    [HarmonyPatch(typeof(PartnerData), "SetDiathesisMp")]
    [HarmonyPrefix]
    public static bool SetDiathesisMp_Prefix(int _value, PartnerData __instance)
    {
        __instance.m_diathesisMp = _value;
        return false;
    }

    [HarmonyPatch(typeof(DigimonCommonData), "SetMpMax")]
    [HarmonyPrefix]
    public static bool SetMpMax_Prefix(int value, DigimonCommonData __instance)
    {
        __instance.m_mpMax = value;
        return false;
    }

    [HarmonyPatch(typeof(DigimonCommonData), "AddMpMax")]
    [HarmonyPrefix]
    public static bool AddMpMax_Prefix(int value, DigimonCommonData __instance)
    {
        __instance.m_mpMax += value;
        return false;
    }

    [HarmonyPatch(typeof(PartnerData), "SetDiathesisForcefulness")]
    [HarmonyPrefix]
    public static bool SetDiathesisForcefulness_Prefix(int _value, PartnerData __instance)
    {
        __instance.m_diathesisForcefulness = _value;
        return false;
    }

    [HarmonyPatch(typeof(DigimonCommonData), "SetForcefulness")]
    [HarmonyPrefix]
    public static bool SetForcefulness_Prefix(int value, DigimonCommonData __instance)
    {
        __instance.m_forcefulness = value;
        return false;
    }

    [HarmonyPatch(typeof(DigimonCommonData), "AddForcefulness")]
    [HarmonyPrefix]
    public static bool AddForcefulness_Prefix(int value, DigimonCommonData __instance)
    {
        __instance.m_forcefulness *= value;
        return false;
    }

    [HarmonyPatch(typeof(PartnerData), "SetDiathesisRobustness")]
    [HarmonyPrefix]
    public static bool SetDiathesisRobustness_Prefix(int _value, PartnerData __instance)
    {
        __instance.m_diathesisRobustness = _value;
        return false;
    }

    [HarmonyPatch(typeof(DigimonCommonData), "SetRobustness")]
    [HarmonyPrefix]
    public static bool SetRobustness_Prefix(int value, DigimonCommonData __instance)
    {
        __instance.m_robustness = value;
        return false;
    }

    [HarmonyPatch(typeof(DigimonCommonData), "AddRobustness")]
    [HarmonyPrefix]
    public static bool AddRobustness_Prefix(int value, DigimonCommonData __instance)
    {
        __instance.m_robustness += value;
        return false;
    }

    [HarmonyPatch(typeof(PartnerData), "SetDiathesisCleverness")]
    [HarmonyPrefix]
    public static bool SetDiathesisCleverness_Prefix(int _value, PartnerData __instance)
    {
        __instance.m_diathesisCleverness = _value;
        return false;
    }

    [HarmonyPatch(typeof(DigimonCommonData), "SetCleverness")]
    [HarmonyPrefix]
    public static bool SetCleverness_Prefix(int value, DigimonCommonData __instance)
    {
        __instance.m_cleverness = value;
        return false;
    }

    [HarmonyPatch(typeof(DigimonCommonData), "AddCleverness")]
    [HarmonyPrefix]
    public static bool AddCleverness_Prefix(int value, DigimonCommonData __instance)
    {
        __instance.m_cleverness += value;
        return false;
    }

    [HarmonyPatch(typeof(PartnerData), "SetDiathesisRapidity")]
    [HarmonyPrefix]
    public static bool SetDiathesisRapidity_Prefix(int _value, PartnerData __instance)
    {
        __instance.m_diathesisRapidity = _value;
        return false;
    }

    [HarmonyPatch(typeof(DigimonCommonData), "SetRapidity")]
    [HarmonyPrefix]
    public static bool SetRapidity_Prefix(int value, DigimonCommonData __instance)
    {
        __instance.m_rapidity = value;
        return false;
    }

    [HarmonyPatch(typeof(DigimonCommonData), "AddRapidity")]
    [HarmonyPrefix]
    public static bool AddRapidity_Prefix(int value, DigimonCommonData __instance)
    {
        __instance.m_rapidity += value;
        return false;
    }
}
