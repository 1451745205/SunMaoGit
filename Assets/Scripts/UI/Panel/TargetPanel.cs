using UnityEngine;
using UnityEngine.UI;
using XFramework;
using XFramework.Extend;

public class TargetPanel : BasePanel
{
    /// <summary>
    /// 路径
    /// </summary>
    static readonly string path = "Prefabs/UI/Panel/TargetPanel";
    /// <summary>
    /// 主面板
    /// </summary>
    public TargetPanel() : base(new UIType(path))
    {

    }

    protected override void InitEvent()
    {
        ActivePanel.GetOrAddComponentInChildren<Button>("BtnClose").onClick.AddListener(() =>
        {
            Pop();
        });
    }
}
