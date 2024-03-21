using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XFramework;
using XFramework.Extend;

public class LevelPanel : BasePanel
{
    /// <summary>
    /// 路径
    /// </summary>
    static readonly string path = "Prefabs/UI/Panel/LevelPanel";
    /// <summary>
    /// 主面板
    /// </summary>
    public LevelPanel() : base(new UIType(path))
    {

    }

    protected override void InitEvent()
    {
        ActivePanel.GetOrAddComponentInChildren<Button>("BtnPlay").onClick.AddListener(() =>
        {
            Debug.Log("正在游玩中");
        });
        ActivePanel.GetOrAddComponentInChildren<Button>("BtnEnter").onClick.AddListener(() =>
        {
            ExchangePos.Instance.MoveAndRotateToTarget();
        });
        ActivePanel.GetOrAddComponentInChildren<Button>("BtnResult").onClick.AddListener(() =>
        {
            ExchangePos.Instance.JudgeResult();
        });
        ActivePanel.GetOrAddComponentInChildren<Button>("BtnExit").onClick.AddListener(() =>
        {
            Game.LoadScene(new SelectLevelScene());
        });
        ActivePanel.GetOrAddComponentInChildren<Button>("BtnRePlay").onClick.AddListener(() =>
        {
            Debug.Log("重新开始");
            Game.LoadScene(new LevelScene());
        });
        ActivePanel.GetOrAddComponentInChildren<Button>("BtnSetting").onClick.AddListener(() =>
        {
            Push(new SettingPanel());
        });
    }
}
