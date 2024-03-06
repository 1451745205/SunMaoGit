using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XFramework;
using XFramework.Extend;

/// <summary>
/// 提示面板
/// </summary>
public class WarningPanel : BasePanel
{
    /// <summary>
    /// 路径
    /// </summary>
    static readonly string path = "Prefabs/UI/Panel/WarningPanel";
    Text txt;
    string content;

    /// <summary>
    /// 提示面板
    /// </summary>
    public WarningPanel(string content) : base(new UIType(path))
    {
        this.content = content;
    }

    protected override void InitEvent()
    {
        txt = ActivePanel.GetOrAddComponentInChildren<Text>("TestContent");
        ActivePanel.GetOrAddComponentInChildren<Button>("BtnExit").onClick.AddListener(() =>
        {
            Pop();
        });
    }

    public override void OnStart()
    {
        base.OnStart();
        txt.text = content;
    }

    public override void OnChange(BasePanel newPanel)
    {
        WarningPanel panel = newPanel as WarningPanel;
        content = panel.content;
    }
}
