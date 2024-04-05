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
    public Sprite targetSprite; // Ä¿±êÍ¼Æ¬
    public string targetIntroduce; // Í¼Æ¬½éÉÜ
    public string targetName; // Í¼Æ¬Ãû×Ö
}
