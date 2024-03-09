using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XFramework;

public class ARScene : SceneState
{
    /// <summary>
    /// AR³¡¾°
    /// </summary>
    public ARScene()
    {
        sceneName = "AR";
    }

    public override void OnEnter()
    {
        panelManager.Push(new ARPanel());
    }
}
