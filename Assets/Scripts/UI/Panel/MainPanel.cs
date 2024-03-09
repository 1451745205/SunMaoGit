using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XFramework;
using XFramework.Extend;

public class MainPanel : BasePanel
{
    /// <summary>
    /// 路径
    /// </summary>
    static readonly string path = "Prefabs/UI/Panel/MainPanel";

    /// <summary>
    /// 主面板
    /// </summary>
    public MainPanel() : base(new UIType(path))
    {

    }

    protected override void InitEvent()
    {
        ActivePanel.GetOrAddComponentInChildren<Button>("BtnGoLevel").onClick.AddListener(() =>
        {
            Push(new SelectLevelPanel());
        });
        ActivePanel.GetOrAddComponentInChildren<Button>("BtnAR").onClick.AddListener(() =>
        {
            Game.LoadScene(new ARScene());
        });
        ActivePanel.GetOrAddComponentInChildren<Button>("BtnExit").onClick.AddListener(() =>
        {
            Game.LoadScene(new StartScene());
        });
        //ActivePanel.GetOrAddComponentInChildren<Button>("BtnSetting").onClick.AddListener(() =>
        //{
        //    Push(new SettingPanel());
        //});
    }
}
