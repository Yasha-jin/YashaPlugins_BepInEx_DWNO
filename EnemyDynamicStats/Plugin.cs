using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;

namespace EnemyDynamicStats;

[BepInPlugin(GUID, PluginName, PluginVersion)]
[BepInProcess("Digimon World Next Order.exe")]
public class Plugin : BasePlugin
{
    internal const string GUID = "Yashajin.DWNO.EnemyDynamicStats";
    internal const string PluginName = "EnemyDynamicStats";
    internal const string PluginVersion = "1.0.0";

    public static ConfigEntry<bool> ChangeStats { get; set; }

    public static ConfigEntry<bool> UsePartnerAverageStats { get; set; }

    public static ConfigEntry<bool> ForceStatsOverride { get; set; }

    public static ConfigEntry<bool> OverrideScriptedEnemy { get; set; }

    public static ConfigEntry<double> StatsMultiplier { get; set; }

    public override void Load()
    {
        // ConfigBinding
        ChangeStats = Config.Bind("#General", "ChangeStats", true, "Whether or not the stats get dynamically changed. Doesn't affect StatsMultiplier.");
        UsePartnerAverageStats = Config.Bind("#General", "UsePartnerAverageStats", true, "If true : EnemyStats = (PartnerLStats + PartnerRStats) / 2\nIf false : EnemyStats = Highest between both Partners");
        ForceStatsOverride = Config.Bind("#General", "ForceStatsOverride", false, "If true : Override all stats regardless of the defaults.\nIf false : Only override if the stats are lower.");
        OverrideScriptedEnemy = Config.Bind("#General", "OverrideScriptedEnemy", false, "If true : Override event/scripted enemy, such as story boss, dimension boss and recrutable.\nIf false : Don't override them.");
        StatsMultiplier = Config.Bind("#General", "StatsMultiplier", 1.0, "Multiply the enemy's final stats. Isn't affected by ChangeStats.\nValue is double, so you can type 1.5 for example.");

        // Plugin startup logic
        Log.LogInfo($"Plugin {GUID} is loaded!");
        Harmony.CreateAndPatchAll(typeof(Plugin));
    }

    [HarmonyPatch(typeof(EnemyCtrl), "Initialize")]
    [HarmonyPostfix]
    public static void Initialize_Postfix(ref DigimonGameData data, ParameterPlacementEnemy placement, EnemyCtrl __instance)
    {
        // "m_vigilance_dist" being 0 mean it's a scripted enemy.
        if (placement.m_vigilance_dist != 0 || OverrideScriptedEnemy.Value)
        {
            DigimonCommonData _partnerCtrlR = MainGameManager.GetPartnerCtrl(0).gameData.m_partnerData.m_commonData;
            DigimonCommonData _partnerCtrlL = MainGameManager.GetPartnerCtrl(1).gameData.m_partnerData.m_commonData;

            int HP = _partnerCtrlR.m_hpMax > _partnerCtrlL.m_hpMax ? _partnerCtrlR.m_hpMax : _partnerCtrlL.m_hpMax;
            int MP = _partnerCtrlR.m_mpMax > _partnerCtrlL.m_mpMax ? _partnerCtrlR.m_mpMax : _partnerCtrlL.m_mpMax;
            int STR = _partnerCtrlR.m_forcefulness > _partnerCtrlL.m_forcefulness ? _partnerCtrlR.m_forcefulness : _partnerCtrlL.m_forcefulness;
            int STA = _partnerCtrlR.m_robustness > _partnerCtrlL.m_robustness ? _partnerCtrlR.m_robustness : _partnerCtrlL.m_robustness;
            int WIS = _partnerCtrlR.m_cleverness > _partnerCtrlL.m_cleverness ? _partnerCtrlR.m_cleverness : _partnerCtrlL.m_cleverness;
            int SPD = _partnerCtrlR.m_rapidity > _partnerCtrlL.m_rapidity ? _partnerCtrlR.m_rapidity : _partnerCtrlL.m_rapidity;

            if (UsePartnerAverageStats.Value)
            {
                HP = (_partnerCtrlR.m_hpMax + _partnerCtrlL.m_hpMax) / 2;
                MP = (_partnerCtrlR.m_mpMax + _partnerCtrlL.m_mpMax) / 2;
                STR = (_partnerCtrlR.m_forcefulness + _partnerCtrlL.m_forcefulness) / 2;
                STA = (_partnerCtrlR.m_robustness + _partnerCtrlL.m_robustness) / 2;
                WIS = (_partnerCtrlR.m_cleverness + _partnerCtrlL.m_cleverness) / 2;
                SPD = (_partnerCtrlR.m_rapidity + _partnerCtrlL.m_rapidity) / 2;
            }

            if (ChangeStats.Value)
            {
                if (data.m_commonData.m_hpMax < HP || ForceStatsOverride.Value)
                    data.m_commonData.m_hpMax = HP;
                if (data.m_commonData.m_mpMax < MP || ForceStatsOverride.Value)
                    data.m_commonData.m_mpMax = MP;
                if (data.m_commonData.m_forcefulness < STR || ForceStatsOverride.Value)
                    data.m_commonData.m_forcefulness = STR;
                if (data.m_commonData.m_robustness < STA || ForceStatsOverride.Value)
                    data.m_commonData.m_robustness = STA;
                if (data.m_commonData.m_cleverness < WIS || ForceStatsOverride.Value)
                    data.m_commonData.m_cleverness = WIS;
                if (data.m_commonData.m_rapidity < SPD || ForceStatsOverride.Value)
                    data.m_commonData.m_rapidity = SPD;
            }

            data.m_commonData.m_hpMax = (int)(data.m_commonData.m_hpMax * StatsMultiplier.Value);
            data.m_commonData.m_mpMax = (int)(data.m_commonData.m_mpMax * StatsMultiplier.Value);
            data.m_commonData.m_forcefulness = (int)(data.m_commonData.m_forcefulness * StatsMultiplier.Value);
            data.m_commonData.m_robustness = (int)(data.m_commonData.m_robustness * StatsMultiplier.Value);
            data.m_commonData.m_cleverness = (int)(data.m_commonData.m_cleverness * StatsMultiplier.Value);
            data.m_commonData.m_rapidity = (int)(data.m_commonData.m_rapidity * StatsMultiplier.Value);

            data.m_commonData.m_hp = data.m_commonData.m_hpMax;
            data.m_commonData.m_mp = data.m_commonData.m_mpMax;
        }
    }
}
