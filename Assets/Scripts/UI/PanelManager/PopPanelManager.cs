using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopPanelManager : MonoBehaviour
{
    private void Start()
    {
        // 查找名为 "PopCanvas" 的画布
        Canvas popCanvas = GameObject.Find("PopCanvas").GetComponent<Canvas>();
        if (popCanvas != null)
        {
            // 将当前脚本所挂载的面板移动到 PopCanvas 画布下
            transform.SetParent(popCanvas.transform, false);
        }
    }
    
    
}
