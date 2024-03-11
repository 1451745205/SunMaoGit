using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XFramework;

public class SelectLevelScene : SceneState
{
    /// <summary>
    /// 选关场景
    /// </summary>
    public SelectLevelScene()
    {
        sceneName = "Main";
    }

    public override void OnEnter()
    {
        panelManager.Push(new SelectLevelPanel());
    }
}
