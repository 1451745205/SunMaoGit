using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopPanelManager : MonoBehaviour
{
    private void Start()
    {
        // ������Ϊ "PopCanvas" �Ļ���
        Canvas popCanvas = GameObject.Find("PopCanvas").GetComponent<Canvas>();
        if (popCanvas != null)
        {
            // ����ǰ�ű������ص�����ƶ��� PopCanvas ������
            transform.SetParent(popCanvas.transform, false);
        }
    }
    
    
}
