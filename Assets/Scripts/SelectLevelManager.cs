using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelManager : MonoBehaviour
{
    public GameObject buttonGroup;

    Button[] levelSelectButtons;   // �ؿ���ť
    int unlocketLevelIndex = 0;   // �ѽ����Ĺؿ�����
    void Start()
    {
        OpenLevel();
    }

    public void OpenLevel()
    {
        Debug.Log("���111��Ŀǰ�ѽ��� 2 ���ؿ�");
        // ���ص�ǰ��ҵĹؿ�����
        string myUsername = UserManager.myUsername;
        unlocketLevelIndex = LevelProgressManager.instance.LoadLevelProgress(myUsername);

        levelSelectButtons = new Button[buttonGroup.transform.childCount];  // ��ȡ�ܹ��Ĺؿ�����

        for (int i = 0; i < buttonGroup.transform.childCount; i++)
        {
            levelSelectButtons[i] = buttonGroup.transform.GetChild(i).GetComponent<Button>();
        }

        // �ر����еİ�ť
        for (int i = 0; i < levelSelectButtons.Length; i++)
        {
            levelSelectButtons[i].interactable = false;

            //�����îģ��ͼƬ
            levelSelectButtons[i].GetComponentInChildren<Transform>().Find("Model").gameObject.SetActive(false);
            //��ʾ����ͼƬ
            levelSelectButtons[i].GetComponentInChildren<Transform>().Find("Lock").gameObject.SetActive(true);
        }

        // ����ÿ����ͨ�صİ�ť
        for (int i = 0; i < unlocketLevelIndex + 1; i++)
        {
            levelSelectButtons[i].interactable = true;

            //��ʾ�îģ��ͼƬ
            levelSelectButtons[i].GetComponentInChildren<Transform>().Find("Model").gameObject.SetActive(true);
            //��������ͼƬ
            levelSelectButtons[i].GetComponentInChildren<Transform>().Find("Lock").gameObject.SetActive(false);
        }
        //Debug.Log(unlocketLevelIndex);
    }
}
