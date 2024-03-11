using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // 假设预制体的路径是 "Assets/Resources/Prefabs/Object_{index}.prefab"，其中 {index} 是整数。
    string prefabDirectory = SelectLevelPanel.levelName;
    string prefabNameFormat = "Model0{0}";
    int startIndex = 1;
    int endIndex = 3; // 或者你想要实例化的预制体数量

    void Start()
    {
        for (int i = startIndex; i <= endIndex; i++)
        {
            // 根据命名规则生成预制体资源的相对路径（去掉 "Assets/Resources/"）
            string prefabRelativePath = Path.Combine("Models/Levels/", prefabDirectory, string.Format(prefabNameFormat, i) );

            // 使用Resources.Load加载预制体
            GameObject prefab = Resources.Load<GameObject>(prefabRelativePath);

            if (prefab)
            {
                // 实例化预制体，并设置其父对象为指定的Transform
                GameObject instance = Instantiate(prefab);
                instance.transform.SetParent(transform, false);

                // 如果需要调整实例的位置或旋转等属性，请在这里添加相关代码
            }
            else
            {
                break;
            }
        }
    }
}
