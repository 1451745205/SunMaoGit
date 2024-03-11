using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XFramework;
using XFramework.Extend;

/// <summary>
/// 
/// </summary>
public class SelectLevelPanel : BasePanel
{
    /// <summary>
    /// 路径
    /// </summary>
    static readonly string path = "Prefabs/UI/Panel/SelectLevelPanel";

    /// <summary>
    /// 
    /// </summary>
    public SelectLevelPanel() : base(new UIType(path))
    {
        
    }

    public static string levelName = "00";  //第几关

    protected override void InitEvent()
    {
        ActivePanel.GetOrAddComponentInChildren<Button>("BtnLevel01").onClick.AddListener(() =>
        {
            Game.LoadScene(new LevelScene());
            levelName = "01";
            Debug.Log("打开了第一关");
        });
        ActivePanel.GetOrAddComponentInChildren<Button>("BtnLevel02").onClick.AddListener(() =>
        {
            Game.LoadScene(new LevelScene());
            levelName = "02";
            Debug.Log("打开了第二关");
        });
        ActivePanel.GetOrAddComponentInChildren<Button>("BtnLevel03").onClick.AddListener(() =>
        {
            Game.LoadScene(new LevelScene());
            levelName = "03";
            Debug.Log("打开了第三关");
        });
        ActivePanel.GetOrAddComponentInChildren<Button>("BtnExit").onClick.AddListener(() =>
        {
            Game.LoadScene(new MainScene());
        });
    }

    public override void OnStart()
    {
        base.OnStart();
    }

    public override void OnChange(BasePanel newPanel)
    {
        SelectLevelPanel panel = newPanel as SelectLevelPanel;
    }
}
