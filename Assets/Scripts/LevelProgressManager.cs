using UnityEngine;

/// <summary>
/// ��ҹؿ����ȹ���
/// </summary>
public class LevelProgressManager : MonoBehaviour
{
    // ����
    public static LevelProgressManager instance;

    void Awake()
    {
        // ȷ��ֻ��һ��ʵ������
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ������ҵĹؿ�����
    public void SaveLevelProgress(string playerName, int levelProgress)
    {
        PlayerPrefs.SetInt(playerName + "_LevelProgress", levelProgress);
    }

    // ������ҵĹؿ�����
    public int LoadLevelProgress(string playerName)
    {
        // ����Ƿ���ڼ�
        if (PlayerPrefs.HasKey(playerName + "_LevelProgress"))
        {
            return PlayerPrefs.GetInt(playerName + "_LevelProgress");
        }
        else
        {
            return 0; // Ĭ��Ϊδ���״̬
        }
    }
}
