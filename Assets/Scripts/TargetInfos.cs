using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Target Info", menuName = "Scripts/Target Info")]
public class TargetInfos : ScriptableObject
{
    public List<TargetInfo> targetInfos = new List<TargetInfo>();
}
[System.Serializable]
public class TargetInfo
{
    public Sprite targetSprite; // Ŀ��ͼƬ
    public string targetIntroduce; // ͼƬ����
    public string targetName; // ͼƬ����
}
