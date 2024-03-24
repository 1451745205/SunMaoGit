using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelManager : MonoBehaviour
{
    public GameObject selectLevelPanel;

    Button[] levelSelectButtons;   // 关卡按钮
    int unlocketLevelIndex;   // 已解锁的关卡数量
    void Start()
    {
        unlocketLevelIndex = PlayerPrefs.GetInt("unlocketLevelIndex");  // 获取持久化的已解锁的关卡数量
        levelSelectButtons = new Button[selectLevelPanel.transform.childCount];  // 获取总共的关卡数量
        
        for(int i =0; i<selectLevelPanel.transform.childCount;i++)
        {
            levelSelectButtons[i] = selectLevelPanel.transform.GetChild(i).GetComponent<Button>();
        }

        // 关闭所有的按钮
        for (int i = 0; i < levelSelectButtons.Length; i++)
        {
            levelSelectButtons[i].interactable = false;  
        }

        // 开启每个已通关的按钮
        for (int i = 0; i < unlocketLevelIndex + 1; i++) 
        {
            levelSelectButtons[i].interactable = true;
        }
    }
}
