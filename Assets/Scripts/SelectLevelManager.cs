using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelManager : MonoBehaviour
{
    public GameObject buttonGroup;

    Button[] levelSelectButtons;   // 关卡按钮
    int unlocketLevelIndex = 0;   // 已解锁的关卡数量
    void Start()
    {
        OpenLevel();
    }

    public void OpenLevel()
    {
        Debug.Log("玩家111，目前已解锁 2 个关卡");
        // 加载当前玩家的关卡进度
        string myUsername = UserManager.myUsername;
        unlocketLevelIndex = LevelProgressManager.instance.LoadLevelProgress(myUsername);

        levelSelectButtons = new Button[buttonGroup.transform.childCount];  // 获取总共的关卡数量

        for (int i = 0; i < buttonGroup.transform.childCount; i++)
        {
            levelSelectButtons[i] = buttonGroup.transform.GetChild(i).GetComponent<Button>();
        }

        // 关闭所有的按钮
        for (int i = 0; i < levelSelectButtons.Length; i++)
        {
            levelSelectButtons[i].interactable = false;

            //隐藏榫卯模型图片
            levelSelectButtons[i].GetComponentInChildren<Transform>().Find("Model").gameObject.SetActive(false);
            //显示锁的图片
            levelSelectButtons[i].GetComponentInChildren<Transform>().Find("Lock").gameObject.SetActive(true);
        }

        // 开启每个已通关的按钮
        for (int i = 0; i < unlocketLevelIndex + 1; i++)
        {
            levelSelectButtons[i].interactable = true;

            //显示榫卯模型图片
            levelSelectButtons[i].GetComponentInChildren<Transform>().Find("Model").gameObject.SetActive(true);
            //隐藏锁的图片
            levelSelectButtons[i].GetComponentInChildren<Transform>().Find("Lock").gameObject.SetActive(false);
        }
        //Debug.Log(unlocketLevelIndex);
    }
}
