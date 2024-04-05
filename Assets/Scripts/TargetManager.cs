using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetManager : MonoBehaviour
{
    public GameObject targetPanel;
    public TargetInfos targetInfos;

    private Text targetName;
    private Text targetIntroduce;
    private Image targetImage;

    private void Awake()
    {
        targetName = targetPanel.transform.Find("TargetName").GetComponent<Text>();
        targetIntroduce = targetPanel.transform.Find("TargetIntroduce").GetComponent<Text>();
        targetImage = targetPanel.transform.Find("TargetImage").GetComponent<Image>();
    }

    private void Start()
    {
        UpdateTargetData();
    }

    public void UpdateTargetData()
    {
        int levelNum = int.Parse(SelectLevelPanel.levelName)-1;
        TargetInfo targetInfo = targetInfos.targetInfos[levelNum];
        targetName.text = targetInfo.targetName;
        targetIntroduce.text = targetInfo.targetIntroduce;
        targetImage.sprite = targetInfo.targetSprite;

    }
}
