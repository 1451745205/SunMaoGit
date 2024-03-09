using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XFramework;
using XFramework.Extend;

public class ARPanel : BasePanel
{
    /// <summary>
    /// 路径
    /// </summary>
    static readonly string path = "Prefabs/UI/Panel/ARPanel";

    /// <summary>
    /// 主面板
    /// </summary>
    public ARPanel() : base(new UIType(path))
    {

    }

    protected override void InitEvent()
    {
        ActivePanel.GetOrAddComponentInChildren<Button>("BtnModel01").onClick.AddListener(() =>
        {
            Debug.Log("打开了第一个模型");
        });
        ActivePanel.GetOrAddComponentInChildren<Button>("BtnModel02").onClick.AddListener(() =>
        {
            Debug.Log("打开了第二个模型");
        });
        ActivePanel.GetOrAddComponentInChildren<Button>("BtnModel03").onClick.AddListener(() =>
        {
            Debug.Log("打开了第三个模型");
        });
        ActivePanel.GetOrAddComponentInChildren<Button>("BtnExit").onClick.AddListener(() =>
        {
            Game.LoadScene(new MainScene());
        });
        //ActivePanel.GetOrAddComponentInChildren<Button>("BtnSetting").onClick.AddListener(() =>
        //{
        //    Push(new SettingPanel());
        //});
    }
}
