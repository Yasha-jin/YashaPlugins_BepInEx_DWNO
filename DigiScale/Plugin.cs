using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using System.Collections.Generic;
using BepInEx.Configuration;

namespace DigiScale;

[BepInPlugin(GUID, PluginName, PluginVersion)]
[BepInProcess("Digimon World Next Order.exe")]
public class Plugin : BasePlugin
{
    internal const string GUID = "BepInEx.YashaPlugins.DWNO.DigiScale";
    internal const string PluginName = "DigiScale";
    internal const string PluginVersion = "1.0";

    public class Digimon
    {
        public uint ID;
        public string Name;
        public string Description;
    }

    public static List<Digimon> Digimons = new List<Digimon>()
    {
        new Digimon(){ID = 324681115, Name = "Botamon"},
        new Digimon(){ID = 324681112, Name = "Punimon"},
        new Digimon(){ID = 324681113, Name = "Poyomon"},
        new Digimon(){ID = 324681118, Name = "YukimiBotamon"},
        new Digimon(){ID = 324681119, Name = "Pabumon"},
        new Digimon(){ID = 324681116, Name = "Jyarimon"},
        new Digimon(){ID = 324681117, Name = "Zerimon"},
        new Digimon(){ID = 324681106, Name = "Conomon"},
        new Digimon(){ID = 324681107, Name = "Kuramon"},
        new Digimon(){ID = 341458701, Name = "Pichimon"},
        new Digimon(){ID = 341458703, Name = "Yuramon"},
        new Digimon(){ID = 341458702, Name = "Botamon", Description = "SPECIAL VOICE LINE"},
        new Digimon(){ID = 341458697, Name = "Punimon", Description = "SPECIAL VOICE LINE"},
        new Digimon(){ID = 2495797070, Name = "Koromon"},
        new Digimon(){ID = 2495797069, Name = "Tsunomon"},
        new Digimon(){ID = 2495797068, Name = "Tokomon"},
        new Digimon(){ID = 2495797066, Name = "Motimon"},
        new Digimon(){ID = 2495797065, Name = "Tanemon"},
        new Digimon(){ID = 2495797064, Name = "Gigimon"},
        new Digimon(){ID = 2495797063, Name = "Gummymon"},
        new Digimon(){ID = 2495797062, Name = "Kokomon"},
        new Digimon(){ID = 2479019484, Name = "Tsumemon"},
        new Digimon(){ID = 2479019485, Name = "Nyaromon"},
        new Digimon(){ID = 2479019486, Name = "Bukamon"},
        new Digimon(){ID = 2479019487, Name = "Koromon", Description = "SPECIAL VOICE LINE"},
        new Digimon(){ID = 2479019480, Name = "Tsunomon", Description = "SPECIAL VOICE LINE"},
        new Digimon(){ID = 888041221, Name = "Agumon"},
        new Digimon(){ID = 888041222, Name = "Gabumon"},
        new Digimon(){ID = 888041223, Name = "Biyomon"},
        new Digimon(){ID = 888041216, Name = "Patamon"},
        new Digimon(){ID = 888041217, Name = "Tentomon"},
        new Digimon(){ID = 888041218, Name = "Gomamon"},
        new Digimon(){ID = 888041219, Name = "Palmon"},
        new Digimon(){ID = 888041228, Name = "Salamon"},
        new Digimon(){ID = 888041229, Name = "Veemon"},
        new Digimon(){ID = 904818934, Name = "Wormmon"},
        new Digimon(){ID = 904818933, Name = "Guilmon"},
        new Digimon(){ID = 904818932, Name = "Terriermon"},
        new Digimon(){ID = 904818931, Name = "Lopmon"},
        new Digimon(){ID = 904818930, Name = "Renamon"},
        new Digimon(){ID = 904818929, Name = "Gaomon"},
        new Digimon(){ID = 921596521, Name = "Hagurumon"},
        new Digimon(){ID = 921596526, Name = "Gotsumon"},
        new Digimon(){ID = 921596527, Name = "Goblimon"},
        new Digimon(){ID = 921596524, Name = "ToyAgumon"},
        new Digimon(){ID = 921596525, Name = "DemiDevimon"},
        new Digimon(){ID = 921596514, Name = "Lucemon"},
        new Digimon(){ID = 921596515, Name = "Hackmon"},
        new Digimon(){ID = 938374109, Name = "Agumon (Black)"},
        new Digimon(){ID = 938374108, Name = "SnowAgumon"},
        new Digimon(){ID = 938374111, Name = "Gabumon (Black)"},
        new Digimon(){ID = 938374110, Name = "Psychemon"},
        new Digimon(){ID = 938374105, Name = "Tsukaimon"},
        new Digimon(){ID = 938374104, Name = "Aruraumon"},
        new Digimon(){ID = 938374101, Name = "ToyAgumon (Black)"},
        new Digimon(){ID = 938374100, Name = "ClearAgumon"},
        new Digimon(){ID = 820930776, Name = "Solarmon"},
        new Digimon(){ID = 820930777, Name = "SnowGoblimon"},
        new Digimon(){ID = 820930778, Name = "Shamamon"},
        new Digimon(){ID = 820930779, Name = "Keramon"},
        new Digimon(){ID = 820930780, Name = "Gumdramon"},
        new Digimon(){ID = 820930781, Name = "Shoutmon"},
        new Digimon(){ID = 820930782, Name = "Agumon", Description = "SPECIAL VOICE LINE"},
        new Digimon(){ID = 820930783, Name = "Gabumon", Description = "SPECIAL VOICE LINE"},
        new Digimon(){ID = 3059157304, Name = "Greymon"},
        new Digimon(){ID = 3059157307, Name = "Garurumon"},
        new Digimon(){ID = 3059157306, Name = "Birdramon"},
        new Digimon(){ID = 3059157309, Name = "Angemon"},
        new Digimon(){ID = 3059157308, Name = "Kabuterimon"},
        new Digimon(){ID = 3059157311, Name = "Ikkakumon"},
        new Digimon(){ID = 3059157310, Name = "Togemon"},
        new Digimon(){ID = 3059157297, Name = "Gatomon"},
        new Digimon(){ID = 3059157296, Name = "ExVeemon"},
        new Digimon(){ID = 3042379591, Name = "Stingmon"},
        new Digimon(){ID = 3042379590, Name = "Growlmon"},
        new Digimon(){ID = 3042379589, Name = "Gargomon"},
        new Digimon(){ID = 3042379586, Name = "Turuiemon"},
        new Digimon(){ID = 3042379587, Name = "Kyubimon"},
        new Digimon(){ID = 3042379584, Name = "GaoGamon"},
        new Digimon(){ID = 3042379598, Name = "Wizardmon"},
        new Digimon(){ID = 3042379599, Name = "Devimon"},
        new Digimon(){ID = 3025602003, Name = "Veedramon"},
        new Digimon(){ID = 3025602002, Name = "Tyrannomon"},
        new Digimon(){ID = 3025602000, Name = "Ogremon"},
        new Digimon(){ID = 3025602007, Name = "Leomon"},
        new Digimon(){ID = 3025602006, Name = "Meramon"},
        new Digimon(){ID = 3025602004, Name = "Vegiemon"},
        new Digimon(){ID = 3025602010, Name = "Nanimon"},
        new Digimon(){ID = 3008824418, Name = "Seadramon"},
        new Digimon(){ID = 3008824419, Name = "Kuwagamon"},
        new Digimon(){ID = 3008824420, Name = "Guardromon"},
        new Digimon(){ID = 3008824423, Name = "Woodmon"},
        new Digimon(){ID = 3008824424, Name = "BomberNanimon"},
        new Digimon(){ID = 3008824425, Name = "Icemon"},
        new Digimon(){ID = 3126267749, Name = "Hyogamon"},
        new Digimon(){ID = 3126267748, Name = "Piddomon"},
        new Digimon(){ID = 3126267751, Name = "Kyubimon (Silver)"},
        new Digimon(){ID = 3126267750, Name = "Gururumon"},
        new Digimon(){ID = 3126267745, Name = "Saberdramon"},
        new Digimon(){ID = 3126267744, Name = "BlackGatomon"},
        new Digimon(){ID = 3126267746, Name = "Fugamon"},
        new Digimon(){ID = 3126267757, Name = "Guardromon (Gold)"},
        new Digimon(){ID = 3126267756, Name = "Youkomon"},
        new Digimon(){ID = 3109490162, Name = "RedVeedramon"},
        new Digimon(){ID = 3109490163, Name = "GoldVeedramon"},
        new Digimon(){ID = 3109490160, Name = "Growlmon (Orange)"},
        new Digimon(){ID = 3109490161, Name = "Greymon (Blue)"},
        new Digimon(){ID = 3109490166, Name = "Garurumon (Black)"},
        new Digimon(){ID = 3109490167, Name = "RedVegiemon"},
        new Digimon(){ID = 3109490164, Name = "IceDevimon"},
        new Digimon(){ID = 3109490165, Name = "Sorcermon"},
        new Digimon(){ID = 3109490170, Name = "Chrysalimon"},
        new Digimon(){ID = 3109490171, Name = "Numemon"},
        new Digimon(){ID = 3092712479, Name = "Geremon"},
        new Digimon(){ID = 3092712478, Name = "Sukamon"},
        new Digimon(){ID = 3092712477, Name = "PlatinumSukamon"},
        new Digimon(){ID = 3092712476, Name = "Arresterdramon", Description = "digivolved from gumdramon ?"},
        new Digimon(){ID = 3092712475, Name = "OmegaShoutmon", Description = "digivolved from shoutmon ?"},
        new Digimon(){ID = 3092712474, Name = "Meicoomon"},
        new Digimon(){ID = 3092712473, Name = "Greymon", Description = "SPECIAL VOICE LINE"},
        new Digimon(){ID = 3092712472, Name = "Garurumon", Description = "SPECIAL VOICE LINE"},
        new Digimon(){ID = 3624844719, Name = "MetalGreymon"},
        new Digimon(){ID = 3624844716, Name = "WereGarurumon"},
        new Digimon(){ID = 3624844717, Name = "Garudamon"},
        new Digimon(){ID = 3624844714, Name = "MagnaAngemon"},
        new Digimon(){ID = 3624844715, Name = "MegaKabuterimon"},
        new Digimon(){ID = 3624844712, Name = "Zudomon"},
        new Digimon(){ID = 3624844713, Name = "Lillymon"},
        new Digimon(){ID = 3624844710, Name = "Angewomon"},
        new Digimon(){ID = 3624844711, Name = "Paildramon"},
        new Digimon(){ID = 3641622272, Name = "WarGrowlmon"},
        new Digimon(){ID = 3641622275, Name = "Rapidmon"},
        new Digimon(){ID = 3641622274, Name = "Antylamon"},
        new Digimon(){ID = 3641622277, Name = "Taomon"},
        new Digimon(){ID = 3641622276, Name = "MachGaogamon"},
        new Digimon(){ID = 3641622278, Name = "AeroVeedramon"},
        new Digimon(){ID = 3641622281, Name = "Myotismon"},
        new Digimon(){ID = 3641622280, Name = "LadyDevimon"},
        new Digimon(){ID = 3591289417, Name = "Mamemon"},
        new Digimon(){ID = 3591289418, Name = "MetalMamemon"},
        new Digimon(){ID = 3591289420, Name = "Lucemon FM"},
        new Digimon(){ID = 3591289421, Name = "MetalTyrannomon"},
        new Digimon(){ID = 3591289422, Name = "SkullGreymon"},
        new Digimon(){ID = 3591289423, Name = "Gigadramon"},
        new Digimon(){ID = 3591289408, Name = "Megadramon"},
        new Digimon(){ID = 3591289409, Name = "IceLeomon"},
        new Digimon(){ID = 3608067131, Name = "GrapLeomon"},
        new Digimon(){ID = 3608067128, Name = "BlueMeramon"},
        new Digimon(){ID = 3608067133, Name = "MegaSeadramon"},
        new Digimon(){ID = 3608067132, Name = "Okuwamon"},
        new Digimon(){ID = 3691955160, Name = "Datamon"},
        new Digimon(){ID = 3691955166, Name = "Meteormon"},
        new Digimon(){ID = 3691955167, Name = "MetalGreymon (Blue)"},
        new Digimon(){ID = 3691955164, Name = "WereGarurumon (Black)"},
        new Digimon(){ID = 3691955165, Name = "MegaKabuterimon (Blue)"},
        new Digimon(){ID = 3691955154, Name = "WarGrowlmon (Orange)"},
        new Digimon(){ID = 3691955155, Name = "BlackWarGrowlmon"},
        new Digimon(){ID = 3708732749, Name = "Rapidmon (Gold)"},
        new Digimon(){ID = 3708732748, Name = "Taomon (Silver)"},
        new Digimon(){ID = 3708732751, Name = "Doumon"},
        new Digimon(){ID = 3708732750, Name = "WaruSeadramon"},
        new Digimon(){ID = 3708732745, Name = "Infermon"},
        new Digimon(){ID = 3708732744, Name = "Etemon"},
        new Digimon(){ID = 3708732747, Name = "Monzaemon"},
        new Digimon(){ID = 3708732741, Name = "Maycrackmon VM"},
        new Digimon(){ID = 3708732740, Name = "Agunimon"},
        new Digimon(){ID = 3658399988, Name = "MetalGreymon", Description = "SPECIAL VOICE LINE"},
        new Digimon(){ID = 3658399989, Name = "WereGarurumon", Description = "SPECIAL VOICE LINE"},
        new Digimon(){ID = 1480204994, Name = "WarGreymon"},
        new Digimon(){ID = 1480204993, Name = "ShineGreymon"},
        new Digimon(){ID = 1480204992, Name = "MetalGarurumon"},
        new Digimon(){ID = 1480204998, Name = "Phoenixmon"},
        new Digimon(){ID = 1480204997, Name = "Seraphimon"},
        new Digimon(){ID = 1480204996, Name = "HerculesKabuterimon"},
        new Digimon(){ID = 1480205003, Name = "Vikemon"},
        new Digimon(){ID = 1480205002, Name = "Rosemon"},
        new Digimon(){ID = 1463427408, Name = "Magnadramon"},
        new Digimon(){ID = 1463427409, Name = "MarineAngemon"},
        new Digimon(){ID = 1463427410, Name = "Magnamon"},
        new Digimon(){ID = 1463427412, Name = "Gallantmon"},
        new Digimon(){ID = 1463427413, Name = "MegaGargomon"},
        new Digimon(){ID = 1463427414, Name = "Cherubimon"},
        new Digimon(){ID = 1463427415, Name = "Sakuyamon"},
        new Digimon(){ID = 1463427416, Name = "MirageGaogamon"},
        new Digimon(){ID = 1513760297, Name = "UlforceVeedramon"},
        new Digimon(){ID = 1513760296, Name = "VenomMyotismon"},
        new Digimon(){ID = 1513760299, Name = "Lilithmon"},
        new Digimon(){ID = 1513760298, Name = "Justimon"},
        new Digimon(){ID = 1513760300, Name = "Piedmon"},
        new Digimon(){ID = 1513760303, Name = "Lucemon SM"},
        new Digimon(){ID = 1513760302, Name = "RustTyranomon"},
        new Digimon(){ID = 1513760289, Name = "Samudramon"},
        new Digimon(){ID = 1513760288, Name = "Machinedramon"},
        new Digimon(){ID = 1496982710, Name = "Beelzemon"},
        new Digimon(){ID = 1496982708, Name = "BanchoLeomon"},
        new Digimon(){ID = 1496982709, Name = "Leviamon"},
        new Digimon(){ID = 1496982706, Name = "LordKnightmon"},
        new Digimon(){ID = 1496982707, Name = "Boltmon"},
        new Digimon(){ID = 1496982704, Name = "Creepymon"},
        new Digimon(){ID = 1496982719, Name = "MetalSeadramon"},
        new Digimon(){ID = 1547315471, Name = "GranKuwagamon"},
        new Digimon(){ID = 1547315469, Name = "Belphemon SM"},
        new Digimon(){ID = 1547315468, Name = "Barbamon"},
        new Digimon(){ID = 1547315467, Name = "Examon"},
        new Digimon(){ID = 1547315466, Name = "Titamon"},
        new Digimon(){ID = 1547315465, Name = "Gankoomon"},
        new Digimon(){ID = 1547315464, Name = "Kentaurosmon"},
        new Digimon(){ID = 1547315463, Name = "Craniamon"},
        new Digimon(){ID = 1547315462, Name = "BlackWarGreymon"},
        new Digimon(){ID = 1530537884, Name = "MetalGarurumon (Black)"},
        new Digimon(){ID = 1530537885, Name = "ChaosGallantmon"},
        new Digimon(){ID = 1530537886, Name = "Kuzuhamon"},
        new Digimon(){ID = 1530537887, Name = "Diaboromon"},
        new Digimon(){ID = 1530537880, Name = "MetalEtemon"},
        new Digimon(){ID = 1530537881, Name = "PlatinumNumemon"},
        new Digimon(){ID = 1530537882, Name = "Ophanimon"},
        new Digimon(){ID = 1530537883, Name = "Imperialdramon DM"},
        new Digimon(){ID = 1530537877, Name = "Leopardmon"},
        new Digimon(){ID = 1580870741, Name = "Dynasmon"},
        new Digimon(){ID = 1580870740, Name = "Jijimon"},
        new Digimon(){ID = 1580870743, Name = "KingSukamon", Description = "BUGGY"},
        new Digimon(){ID = 1580870742, Name = "Mastemon"},
        new Digimon(){ID = 1580870737, Name = "Alphamon"},
        new Digimon(){ID = 1580870736, Name = "Jesmon"},
        new Digimon(){ID = 1580870748, Name = "WarGreymon", Description = "SPECIAL VOICE LINE"},
        new Digimon(){ID = 1564093154, Name = "MetalGarurumon", Description = "SPECIAL VOICE LINE"},
        new Digimon(){ID = 1564093155, Name = "Minervamon"},
        new Digimon(){ID = 1564093152, Name = "Dianamon"},
        new Digimon(){ID = 1564093153, Name = "Darkdramon"},
        new Digimon(){ID = 1564093158, Name = "EmperorGreymon"},
        new Digimon(){ID = 1564093159, Name = "MagnaGarurumon"},
        new Digimon(){ID = 1564093156, Name = "Arresterdramon"},
        new Digimon(){ID = 1564093157, Name = "OmegaShoutmon"},
        new Digimon(){ID = 4188204825, Name = "Omegamon"},
        new Digimon(){ID = 4188204826, Name = "Omegamon Zwart"},
        new Digimon(){ID = 4188204828, Name = "Imperialdramon FM"},
        new Digimon(){ID = 4188204829, Name = "Imperialdramon PM"},
        new Digimon(){ID = 4188204830, Name = "Susanomon"},
        new Digimon(){ID = 4188204831, Name = "Belphemon RM"},
        new Digimon(){ID = 4188204816, Name = "Omegamon Zwart D"},
        new Digimon(){ID = 4188204817, Name = "Armageddemon"},
        new Digimon(){ID = 4204982411, Name = "Omegamon Alter-B"},
        new Digimon(){ID = 4204982408, Name = "Gallantmon CM"},
        new Digimon(){ID = 4204982415, Name = "Omegamon", Description = "SPECIAL VOICE LINE"},
        new Digimon(){ID = 4204982414, Name = "ShineGreymon BM"},
        new Digimon(){ID = 4204982413, Name = "Rosemon Burst Mode"},
        new Digimon(){ID = 4204982412, Name = "Chaosmon"},
        new Digimon(){ID = 4204982403, Name = "Boltboutamon"}
    };

    public static ConfigEntry<bool> ChangePartnerScale { get; set; }

    public static ConfigEntry<bool> ChangeEnemyScale { get; set; }

    public static List<ConfigEntry<float>> configEntries = new List<ConfigEntry<float>>();

    public override void Load()
    {
        // ConfigBinding
        ChangePartnerScale = Config.Bind("#General", "ChangePartnerScale", true, "Change the scale of your partners.");
        ChangeEnemyScale = Config.Bind("#General", "ChangeEnnemyScale", true, "Change the scale of the overworld enemies.");

        foreach (var digimon in Digimons)
        {
            var sp = "";
            if (digimon.Description != null)
                sp = " (SP)";
            configEntries.Add(Config.Bind<float>("Digimons", digimon.Name + sp, 1f, "Change " + digimon.Name + " scale. " + digimon.Description));
        }

        // Plugin startup logic
        Log.LogInfo($"Plugin {GUID} is loaded!");
        Harmony.CreateAndPatchAll(typeof(Plugin));
    }

    [HarmonyPatch(typeof(PartnerCtrl), "Initialize")]
    [HarmonyPostfix]
    public static void PatchPartnerScale(PartnerCtrl __instance)
    {
        if (ChangePartnerScale.Value)
            SetScale(__instance);
    }

    [HarmonyPatch(typeof(PartnerCtrl), "Evolution")]
    [HarmonyPostfix]
    public static void PatchPartnerEvoScale(PartnerCtrl __instance)
    {
        if (ChangePartnerScale.Value)
            SetScale(__instance);
    }

    [HarmonyPatch(typeof(EnemyCtrl), "Initialize")]
    [HarmonyPostfix]
    public static void PatchEnemyScale(EnemyCtrl __instance)
    {
        if (ChangeEnemyScale.Value)
            SetScale(__instance);
    }

    public static void SetScale(DigimonCtrl __instance)
    {
        if (__instance.gameObject.active)
        {
            uint id = __instance.gameData.m_commonData.m_baseID;
            var unit = __instance.transform.GetChild(0).gameObject;

            if (Digimons.Exists(x => x.ID == id))
            {
                Digimon _d = Digimons.Find(x => x.ID == id);

                string sp = "";
                if (_d.Description != null)
                    sp = " (SP)";

                float s = configEntries.Find(x => x.Definition.Key == _d.Name + sp).Value;

                if (s > 1f)
                {
                    __instance.transform.localScale = new UnityEngine.Vector3(1f / s, 1f / s, 1f / s);
                    unit.transform.localScale = new UnityEngine.Vector3(s * s, s * s, s * s);
                }
                else if (s < 1f)
                {
                    __instance.transform.localScale = new UnityEngine.Vector3(1f * s, 1f * s, 1f * s);
                    unit.transform.localScale = new UnityEngine.Vector3(1f, 1f, 1f);
                    __instance.GetComponentInChildren<CharacterMove>().m_DefaultAgentRadius *= s;
                }
            }
            else
            {
                __instance.transform.localScale = new UnityEngine.Vector3(1f, 1f, 1f);
                unit.transform.localScale = new UnityEngine.Vector3(1f, 1f, 1f);
            }
        }
        
    }
}
