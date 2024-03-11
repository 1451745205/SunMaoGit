using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // ����Ԥ�����·���� "Assets/Resources/Prefabs/Object_{index}.prefab"������ {index} ��������
    string prefabDirectory = SelectLevelPanel.levelName;
    string prefabNameFormat = "Model0{0}";
    int startIndex = 1;
    int endIndex = 3; // ��������Ҫʵ������Ԥ��������

    void Start()
    {
        for (int i = startIndex; i <= endIndex; i++)
        {
            // ����������������Ԥ������Դ�����·����ȥ�� "Assets/Resources/"��
            string prefabRelativePath = Path.Combine("Models/Levels/", prefabDirectory, string.Format(prefabNameFormat, i) );

            // ʹ��Resources.Load����Ԥ����
            GameObject prefab = Resources.Load<GameObject>(prefabRelativePath);

            if (prefab)
            {
                // ʵ����Ԥ���壬�������丸����Ϊָ����Transform
                GameObject instance = Instantiate(prefab);
                instance.transform.SetParent(transform, false);

                // �����Ҫ����ʵ����λ�û���ת�����ԣ��������������ش���
            }
            else
            {
                break;
            }
        }
    }
}
