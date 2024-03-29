﻿using BepInEx;
using BepInEx.Unity.IL2CPP;
using Cpp2IL.Core.Extensions;
using HarmonyLib;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace ShowHiddenParameters;

[BepInPlugin(GUID, PluginName, PluginVersion)]
[BepInProcess("Digimon World Next Order.exe")]
public class Plugin : BasePlugin
{
    internal const string GUID = "Yashajin.DWNO.ShowHiddenParameters";
    internal const string PluginName = "ShowHiddenParameters";
    internal const string PluginVersion = "1.0.3";

    public static UnityEngine.GameObject BattleWinText;

    public static UnityEngine.GameObject TrainingFailureText;

    public static UnityEngine.GameObject MoodBase;

    public static UnityEngine.GameObject MoodValue;

    public static UnityEngine.GameObject DisciplineBase;

    public static UnityEngine.GameObject DisciplineValue;

    public static UnityEngine.GameObject CurseBase;

    public static UnityEngine.GameObject CurseValue;

    public static UnityEngine.GameObject FatigueBase;

    public static UnityEngine.GameObject FatigueValue;

    public override void Load()
    {
        // Plugin startup logic
        Log.LogInfo($"Plugin {GUID} is loaded!");
        Harmony.CreateAndPatchAll(typeof(Plugin));
    }
    public static Texture2D LoadEmbeddedTexture(string Namespace, string texturePath)
    {
        Assembly _assembly = Assembly.GetExecutingAssembly();
        Stream _stream = _assembly.GetManifestResourceStream(Namespace + "." + texturePath.Replace("/", "."));
        byte[] fileData = _stream.ReadBytes();
        Texture2D tex = new Texture2D(4, 4, TextureFormat.RGBA32, false);
        tex.LoadImage(fileData, false);
        tex.filterMode = FilterMode.Point;
        tex.wrapMode = TextureWrapMode.Clamp;
        return tex;
    }

    [HarmonyPatch(typeof(uPartnerStatusPanelStatus), "Initialize")]
    [HarmonyPostfix]
    public static void Initialize_Postfix(uPartnerStatusPanelStatus __instance)
    {
        Transform _gauges = __instance.transform.Find("Base").transform.Find("Image").transform.Find("Image10").transform;
        _gauges.GetComponent<UnityEngine.RectTransform>().anchoredPosition += new Vector2(25, 0);
        _gauges.GetComponent<UnityEngine.RectTransform>().sizeDelta += new Vector2(50, 0);

        InitBases(__instance);
        InitTexts(__instance);
        InitValues(__instance);
    }

    public static void InitBases(uPartnerStatusPanelStatus __instance)
    {
        Transform _base = __instance.transform.Find("Base").transform.Find("Image").transform;
        GameObject _baseCopy = _base.Find("Number_Base_1").gameObject;

        UnityEngine.Texture2D BaseTexture = LoadEmbeddedTexture(typeof(Plugin).Namespace, "Assets/Base_3.png");
        UnityEngine.Sprite BaseSprite = Sprite.Create(BaseTexture, new Rect(0, 0, BaseTexture.width, BaseTexture.height), new Vector2(0.5f, 0.5f));

        MoodBase = UnityEngine.Object.Instantiate(_baseCopy);
        MoodBase.name = "MoodBase";
        MoodBase.transform.SetParent(_base);
        MoodBase.transform.localScale = _baseCopy.transform.localScale;
        MoodBase.transform.localPosition = _baseCopy.transform.localPosition + new Vector3(250.8f, 0, 0);
        MoodBase.GetComponent<UnityEngine.UI.Image>().m_Sprite = BaseSprite;
        MoodBase.GetComponent<UnityEngine.RectTransform>().sizeDelta = new Vector2(45, 32);

        DisciplineBase = UnityEngine.Object.Instantiate(_baseCopy);
        DisciplineBase.name = "DisciplineBase";
        DisciplineBase.transform.SetParent(_base);
        DisciplineBase.transform.localScale = _baseCopy.transform.localScale;
        DisciplineBase.transform.localPosition = MoodBase.transform.localPosition + new Vector3(0, -30, 0);
        DisciplineBase.GetComponent<UnityEngine.UI.Image>().m_Sprite = BaseSprite;
        DisciplineBase.GetComponent<UnityEngine.RectTransform>().sizeDelta = new Vector2(45, 32);

        CurseBase = UnityEngine.Object.Instantiate(_baseCopy);
        CurseBase.name = "CurseBase";
        CurseBase.transform.SetParent(_base);
        CurseBase.transform.localScale = _baseCopy.transform.localScale;
        CurseBase.transform.localPosition = DisciplineBase.transform.localPosition + new Vector3(0, -30, 0);
        CurseBase.GetComponent<UnityEngine.UI.Image>().m_Sprite = BaseSprite;
        CurseBase.GetComponent<UnityEngine.RectTransform>().sizeDelta = new Vector2(45, 32);

        FatigueBase = UnityEngine.Object.Instantiate(_baseCopy);
        FatigueBase.name = "FatigueBase";
        FatigueBase.transform.SetParent(_base);
        FatigueBase.transform.localScale = _baseCopy.transform.localScale;
        FatigueBase.transform.localPosition = CurseBase.transform.localPosition + new Vector3(0, -30, 0);
        FatigueBase.GetComponent<UnityEngine.UI.Image>().m_Sprite = BaseSprite;
        FatigueBase.GetComponent<UnityEngine.RectTransform>().sizeDelta = new Vector2(45, 32);
    }

    public static void InitTexts(uPartnerStatusPanelStatus __instance)
    {
        Transform _text = __instance.transform.Find("Base").transform.Find("Text").transform;
        GameObject _textCopy = _text.Find("Attack").gameObject;

        BattleWinText = UnityEngine.Object.Instantiate(_textCopy);
        BattleWinText.name = "BattleWin";

        TrainingFailureText = UnityEngine.Object.Instantiate(_textCopy);
        TrainingFailureText.name = "TrainingFailure";

        BattleWinText.transform.SetParent(_text);
        BattleWinText.transform.localPosition = _textCopy.transform.localPosition + new Vector3(393f, -60, 0);
        BattleWinText.transform.localScale = _textCopy.transform.localScale;
        BattleWinText.GetComponent<UnityEngine.UI.Text>().alignment = UnityEngine.TextAnchor.MiddleLeft;

        TrainingFailureText.transform.SetParent(_text);
        TrainingFailureText.transform.localPosition = BattleWinText.transform.localPosition + new Vector3(0, -30, 0);
        TrainingFailureText.transform.localScale = _textCopy.transform.localScale;
        TrainingFailureText.GetComponent<UnityEngine.UI.Text>().alignment = UnityEngine.TextAnchor.MiddleLeft;
    }

    public static void InitValues(uPartnerStatusPanelStatus __instance)
    {
        Transform _value = __instance.transform.Find("Value").transform;
        GameObject _valueCopy = _value.Find("Attack").gameObject;

        MoodValue = UnityEngine.Object.Instantiate(_valueCopy);
        MoodValue.name = "MoodValue";
        MoodValue.transform.SetParent(_value);
        MoodValue.transform.localScale = _valueCopy.transform.localScale;
        MoodValue.transform.localPosition = _valueCopy.transform.localPosition + new Vector3(252f, 0, 0);

        DisciplineValue = UnityEngine.Object.Instantiate(_valueCopy);
        DisciplineValue.name = "DisciplineValue";
        DisciplineValue.transform.SetParent(_value);
        DisciplineValue.transform.localScale = _valueCopy.transform.localScale;
        DisciplineValue.transform.localPosition = MoodValue.transform.localPosition + new Vector3(0, -30, 0);

        CurseValue = UnityEngine.Object.Instantiate(_valueCopy);
        CurseValue.name = "CurseValue";
        CurseValue.transform.SetParent(_value);
        CurseValue.transform.localScale = _valueCopy.transform.localScale;
        CurseValue.transform.localPosition = DisciplineValue.transform.localPosition + new Vector3(0, -30, 0);

        FatigueValue = UnityEngine.Object.Instantiate(_valueCopy);
        FatigueValue.name = "FatigueValue";
        FatigueValue.transform.SetParent(_value);
        FatigueValue.transform.localScale = _valueCopy.transform.localScale;
        FatigueValue.transform.localPosition = CurseValue.transform.localPosition + new Vector3(0, -30, 0);
    }

    [HarmonyPatch(typeof(uPartnerStatusPanelStatus), "SetStatus")]
    [HarmonyPostfix]
    public static void SetStatus_Postfix(int selectPartner, uPartnerStatusPanelStatus __instance)
    {
        // They are somehow inverted ???
        int ActualSelectedPartner = selectPartner == 0 ? 1 : 0;
        PartnerCtrl _partnerCtrl = MainGameManager.GetPartnerCtrl(ActualSelectedPartner);

        MoodValue.GetComponent<UnityEngine.UI.Text>().text = _partnerCtrl.gameData.m_partnerData.m_mood.ToString();
        DisciplineValue.GetComponent<UnityEngine.UI.Text>().text = _partnerCtrl.gameData.m_partnerData.m_breeding.ToString();
        CurseValue.GetComponent<UnityEngine.UI.Text>().text = _partnerCtrl.gameData.m_partnerData.m_curse.ToString();
        FatigueValue.GetComponent<UnityEngine.UI.Text>().text = _partnerCtrl.gameData.m_partnerData.m_fatigue.ToString();

        // Lazy to use "Value" style text because of position/scale
        BattleWinText.GetComponent<UnityEngine.UI.Text>().text = "Victory : " + _partnerCtrl.gameData.m_partnerData.m_battleWin.ToString();
        TrainingFailureText.GetComponent<UnityEngine.UI.Text>().text = "TF : " + _partnerCtrl.gameData.m_partnerData.m_trainingFailure.ToString();
    }
}
