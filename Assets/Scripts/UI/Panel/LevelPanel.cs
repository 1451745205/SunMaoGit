using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XFramework;
using XFramework.Extend;

/// <summary>
/// 
/// </summary>
public class LevelPanel : BasePanel
{
    /// <summary>
    /// 路径
    /// </summary>
    static readonly string path = "Prefabs/UI/Panel/LevelPanel";

    /// <summary>
    /// 
    /// </summary>
    public LevelPanel() : base(new UIType(path))
    {
        
    }

    protected override void InitEvent()
    {
        ActivePanel.GetOrAddComponentInChildren<Button>("BtnExit").onClick.AddListener(() =>
        {
            Pop();
        });
    }

    public override void OnStart()
    {
        base.OnStart();
    }

    public override void OnChange(BasePanel newPanel)
    {
        LevelPanel panel = newPanel as LevelPanel;
    }
}
