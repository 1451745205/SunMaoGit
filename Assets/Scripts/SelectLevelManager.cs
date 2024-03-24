using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelManager : MonoBehaviour
{
    public GameObject selectLevelPanel;

    Button[] levelSelectButtons;   // �ؿ���ť
    int unlocketLevelIndex;   // �ѽ����Ĺؿ�����
    void Start()
    {
        unlocketLevelIndex = PlayerPrefs.GetInt("unlocketLevelIndex");  // ��ȡ�־û����ѽ����Ĺؿ�����
        levelSelectButtons = new Button[selectLevelPanel.transform.childCount];  // ��ȡ�ܹ��Ĺؿ�����
        
        for(int i =0; i<selectLevelPanel.transform.childCount;i++)
        {
            levelSelectButtons[i] = selectLevelPanel.transform.GetChild(i).GetComponent<Button>();
        }

        // �ر����еİ�ť
        for (int i = 0; i < levelSelectButtons.Length; i++)
        {
            levelSelectButtons[i].interactable = false;  
        }

        // ����ÿ����ͨ�صİ�ť
        for (int i = 0; i < unlocketLevelIndex + 1; i++) 
        {
            levelSelectButtons[i].interactable = true;
        }
    }
}
