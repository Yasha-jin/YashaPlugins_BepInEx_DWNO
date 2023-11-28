using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace DigimonDubCriesExpansion;

[BepInPlugin(GUID, PluginName, PluginVersion)]
[BepInProcess("Digimon World Next Order.exe")]
public class Plugin : BasePlugin
{
    internal const string GUID = "DigimonDubCriesExpansion";
    internal const string PluginName = "DigimonDubCriesExpansion";
    internal const string PluginVersion = "1.0.0";

    public class Digimon
    {
        public uint ID;
        public int CrySE;
    }

    public static int MaxSE = 500;
    public static List<Digimon> DigimonCry = new List<Digimon>()
    {
        new Digimon(){ID = 888041221, CrySE = 75},
        new Digimon(){ID = 820930782, CrySE = 75},
        new Digimon(){ID = 888041222, CrySE = 76},
        new Digimon(){ID = 820930783, CrySE = 76},
        new Digimon(){ID = 888041219, CrySE = 77},
        new Digimon(){ID = 888041223, CrySE = 78},
        new Digimon(){ID = 888041217, CrySE = 79},
        new Digimon(){ID = 888041218, CrySE = 80},
        new Digimon(){ID = 888041216, CrySE = 81},
        new Digimon(){ID = 3059157304, CrySE = 82},
        new Digimon(){ID = 3092712473, CrySE = 82},
        new Digimon(){ID = 3059157307, CrySE = 83},
        new Digimon(){ID = 3092712472, CrySE = 83},
        new Digimon(){ID = 3059157310, CrySE = 84},
        new Digimon(){ID = 3059157306, CrySE = 85},
        new Digimon(){ID = 3059157308, CrySE = 86},
        new Digimon(){ID = 3059157311, CrySE = 87},
        new Digimon(){ID = 3059157309, CrySE = 88},
        new Digimon(){ID = 3059157297, CrySE = 89},
        new Digimon(){ID = 3624844719, CrySE = 90},
        new Digimon(){ID = 3658399988, CrySE = 90},
        new Digimon(){ID = 3624844716, CrySE = 91},
        new Digimon(){ID = 3658399989, CrySE = 91},
        new Digimon(){ID = 3624844713, CrySE = 92},
        new Digimon(){ID = 3624844717, CrySE = 93},
        new Digimon(){ID = 3624844715, CrySE = 94},
        new Digimon(){ID = 3624844712, CrySE = 95},
        new Digimon(){ID = 3624844714, CrySE = 96},
        new Digimon(){ID = 3624844710, CrySE = 97},
        new Digimon(){ID = 1480204994, CrySE = 98},
        new Digimon(){ID = 1580870748, CrySE = 98},
        new Digimon(){ID = 1480204992, CrySE = 99},
        new Digimon(){ID = 1564093154, CrySE = 99},
        new Digimon(){ID = 4188204825, CrySE = 100},
        new Digimon(){ID = 4204982415, CrySE = 100},
        new Digimon(){ID = 4188204826, CrySE = 100},
        new Digimon(){ID = 4188204816, CrySE = 100},
        new Digimon(){ID = 4204982411, CrySE = 100},
        new Digimon(){ID = 938374109, CrySE = 101},
        new Digimon(){ID = 938374111, CrySE = 102},
        new Digimon(){ID = 3109490161, CrySE = 103},
        new Digimon(){ID = 3109490166, CrySE = 104},
        new Digimon(){ID = 3691955167, CrySE = 105},
        new Digimon(){ID = 3691955164, CrySE = 106},
        new Digimon(){ID = 1547315462, CrySE = 107},
        new Digimon(){ID = 1530537884, CrySE = 108},
        new Digimon(){ID = 888041229, CrySE = 109},
        new Digimon(){ID = 3059157296, CrySE = 110},
        new Digimon(){ID = 904818934, CrySE = 111},
        new Digimon(){ID = 3042379591, CrySE = 112},
    };

    public static int TempCryId = -1;
    public static PartnerCtrl PartnerRef = null;
    public static bool IsFusion = false;

    private static ConfigEntry<bool> PlayCustomCryEverywhere { get; set; }

    public override void Load()
    {
        PlayCustomCryEverywhere = Config.Bind("General", "PlayCustomCryEverywhere", false, "Play the custom cry everywhere instead of only during the evolution.");
        Harmony.CreateAndPatchAll(typeof(Plugin));
    }

    [HarmonyPatch(typeof(AppMainScript), "_FinishedParameterLoad")]
    [HarmonyPrefix]
    public static void _FinishedParameterLoad_Prefix(AppMainScript __instance)
    {
        if (PlayCustomCryEverywhere.Value)
        {
            foreach (ParameterDigimonData param in __instance.m_parameters.digimonData.m_params)
            {
                Digimon digimon = DigimonCry.Find(x => x.ID == param.m_id);
                if (digimon != null)
                {
                    param.m_crySe = digimon.CrySE;
                }
            }
        }
    }

    [HarmonyPatch(typeof(PartnerCtrl), "StartEvolution", new System.Type[] { typeof(uint), typeof(bool) })]
    [HarmonyPrefix]
    public static void StartEvolution_Prefix(uint evo_id, PartnerCtrl __instance)
    {
        foreach (ParameterDigimonData param in AppMainScript.m_instance.m_parameters.digimonData.m_params)
        {
            if (evo_id == param.m_id)
            {
                Digimon digimon = DigimonCry.Find(x => x.ID == param.m_id);
                if (digimon != null)
                {
                    TempCryId = param.m_crySe;
                    param.m_crySe = digimon.CrySE;
                    PartnerRef = __instance;
                }
                break;
            }
        }
    }

    [HarmonyPatch(typeof(PartnerCtrl), "ProductionMiracle")]
    [HarmonyPrefix]
    public static void ProductionMiracle_Prefix(bool is_jogless, ParameterDigimonData digi_data, PartnerCtrl __instance)
    {
        foreach (ParameterDigimonData param in AppMainScript.m_instance.m_parameters.digimonData.m_params)
        {
            if (digi_data.m_id == param.m_id)
            {
                Digimon digimon = DigimonCry.Find(x => x.ID == param.m_id);
                if (digimon != null)
                {
                    TempCryId = param.m_crySe;
                    param.m_crySe = digimon.CrySE;
                    IsFusion = true;
                    CriSoundManager.AddCrySeCueSheet(digimon.CrySE);
                    if (is_jogless)
                        PartnerRef = __instance;
                    else
                        PartnerRef = MainGameManager.GetPartnerCtrl(2);
                }
                break;
            }
        }
    }

    [HarmonyPatch(typeof(EvolutionBase), "InitializeFcmd")]
    [HarmonyPrefix]
    public static void InitializeFcmd_Prefix(EvolutionBase __instance)
    {
        // The Miracle scene camera doesn't have CriAtomListener, which is needed to hear cry sound.
        if (IsFusion)
            __instance.m_camera.gameObject.AddComponent<CriAtomListener>();
    }

    [HarmonyPatch(typeof(EvolutionBase._DestroyTask_d__77), "MoveNext")]
    [HarmonyPostfix]
    public static void EvolutionBase__DestroyTask_d__77_MoveNext_Postfix(EvolutionBase._DestroyTask_d__77 __instance)
    {
        if (__instance.__1__state == 1 && TempCryId != -1)
        {
            foreach (ParameterDigimonData param in AppMainScript.m_instance.m_parameters.digimonData.m_params)
            {
                if (PartnerRef.m_data.m_commonData.m_baseID == param.m_id)
                {
                    CriSoundManager.RemoveCrySeCueSheet(param.m_crySe);
                    param.m_crySe = TempCryId;
                    CriSoundManager.AddCrySeCueSheet(TempCryId);
                    CriSoundManager.RemoveComponentSource(PartnerRef.m_crySeSource);
                    SoundSource ss = CriSoundManager.AddComponentCrySeSource(PartnerRef.gameObject, TempCryId);
                    PartnerRef.m_crySeSource = ss;
                    PartnerRef.m_cryCueSheetNo = param.m_crySe;
                    TempCryId = -1;
                    IsFusion = false;
                    break;
                }
            }
        }
    }

    [HarmonyPatch(typeof(CriSoundManager), "Start")]
    [HarmonyPrefix]
    public static void Start_Prefix()
    {
        CriSoundManager.m_cryCueSheetEntryCount = new Il2CppStructArray<int>(MaxSE);
    }

    [HarmonyPatch(typeof(CriSoundManager), "IsContainCueInCry")]
    [HarmonyPrefix]
    public static bool IsContainCueInCry_Prefix(int _sheetId, int _cueNo, ref bool __result)
    {
        string padding = _sheetId < 10 ? "0" : "";
        CriAtomCueSheet cueSheet = CriAtom.GetCueSheet("V_" + padding + _sheetId.ToString());
        if (cueSheet != null)
        {
            CriAtomExAcb acb = cueSheet.acb;
            if (acb != null)
            {   
                __result = true;
                return false;
            }
        }
        return true;
    }

    // This is the cry function enemy digimon use.
    [HarmonyPatch(typeof(DigimonCtrl), "PlayCrySe")]
    [HarmonyPrefix]
    public static bool PlayCrySe_Prefix(DigimonCtrl __instance, string cueName)
    {
        if (__instance.m_crySeSource != null)
        {
            __instance.m_crySeSource.Stop();
            __instance.m_crySeSource.Play(int.Parse(cueName.Substring(cueName.Length - 2)) - 1);
        }
        return false;
    }

    [HarmonyPatch(typeof(CriSoundManager), "AddCrySeCueSheet")]
    [HarmonyPrefix]
    public static bool AddCrySeCueSheet_Prefix(int _sheetId)
    {
        int num = _sheetId - 1;
        CriSoundManager.m_cryCueSheetEntryCount[num]++;
        string sheetName = CriSoundManager.IdToName(_sheetId);
        CriSoundManager.AddCueSheet(sheetName, sheetName, sheetName);
        return false;
    }

    [HarmonyPatch(typeof(CriSoundManager), "RemoveCrySeCueSheet")]
    [HarmonyPrefix]
    public static bool RemoveCrySeCueSheet_Prefix(int _sheetId)
    {
        if (_sheetId != 0)
        {
            int num = _sheetId - 1;
            CriSoundManager.m_cryCueSheetEntryCount[num]--;
            if (CriSoundManager.m_cryCueSheetEntryCount[num] <= 0)
            {
                string sheetName = CriSoundManager.IdToName(_sheetId);
                CriSoundManager.RemoveCueSheet(sheetName);
                CriSoundManager.m_cryCueSheetEntryCount[num] = 0;
            }
        }
        
        return false;
    }

    [HarmonyPatch(typeof(CriSoundManager), "AddComponentCrySeSource")]
    [HarmonyPrefix]
    public static bool AddComponentCrySeSource_Prefix(ref SoundSource __result, GameObject addObject, int _cueSheetNo, [Optional] string cueName)
    {
        string padding = _cueSheetNo < 10 ? "0" : "";
        string text = "V_" + padding + _cueSheetNo.ToString();
        __result = CriSoundManager.AddComponentSeSource(addObject, text, cueName, true);
        return false;
    }

    [HarmonyPatch(typeof(CriSoundManager), "AddComponentSeSource")]
    [HarmonyPrefix]
    public static bool AddComponentSeSource_Prefix(ref SoundSource __result, GameObject addObject, string cueSheetName, [Optional] string cueName, bool use3D = true)
    {
        SoundSource soundSource = addObject.AddComponent<SoundSource>();
        soundSource._cueSheet = cueSheetName;
        if (cueName != null)
            soundSource._cueName = cueName;
        float currentSeVolume = CriSoundManager.m_currentSeVolume;
        soundSource.volume = currentSeVolume;
        soundSource.use3dPositioning = use3D;
        soundSource._playOnStart = false;
        CriSoundManager.m_characteristicSource.Add(soundSource);
        __result = soundSource;
        return false;
    }

    [HarmonyPatch(typeof(CriSoundManager.__c__DisplayClass88_0), "_changeVoiceLanguageInternal_b__0")]
    [HarmonyPrefix]
    public static void _changeVoiceLanguageInternal_b__0_Prefix(int rtnrval)
    {
        if (rtnrval == 0)
        {
            for (int i = 74; i < MaxSE; i++)
            {
                if (CriSoundManager.m_cryCueSheetEntryCount[i] > 0 && CriSoundManager._IsValidCrySeCueSheetId(i + 1))
                {
                    string text = CriSoundManager.IdToName(i + 1);
                    CriSoundManager.AddCueSheet(text, text, text);
                }
            }
        }
    }

    [HarmonyPatch(typeof(CriSoundManager.__c), "_Start_b__83_1")]
    [HarmonyPrefix]
    public static bool _Start_b__83_1_Prefix(int rtnrval)
    {
        if (rtnrval == 0)
        {
            for (int i = 0; i < MaxSE; i++)
            {
                CriSoundManager.m_cryCueSheetEntryCount[i] = 0;
                CriSoundManager.RemoveCrySeCueSheet(i);
            }
        }
        return false;
    }
}