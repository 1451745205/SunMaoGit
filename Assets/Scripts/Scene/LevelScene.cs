using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XFramework;

public class LevelScene : SceneState
{
    /// <summary>
    /// �ؿ�����
    /// </summary>
    public LevelScene()
    {
        sceneName = "Level";
    }

    public override void OnEnter()
    {
        panelManager.Push(new LevelPanel());
    }
}
