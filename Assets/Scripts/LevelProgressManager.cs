using UnityEngine;

/// <summary>
/// 玩家关卡进度管理
/// </summary>
public class LevelProgressManager : MonoBehaviour
{
    // 单例
    public static LevelProgressManager instance;

    void Awake()
    {
        // 确保只有一个实例存在
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

    // 保存玩家的关卡进度
    public void SaveLevelProgress(string playerName, int levelProgress)
    {
        PlayerPrefs.SetInt(playerName + "_LevelProgress", levelProgress);
    }

    // 加载玩家的关卡进度
    public int LoadLevelProgress(string playerName)
    {
        // 检查是否存在键
        if (PlayerPrefs.HasKey(playerName + "_LevelProgress"))
        {
            return PlayerPrefs.GetInt(playerName + "_LevelProgress");
        }
        else
        {
            return 0; // 默认为未完成状态
        }
    }
}
