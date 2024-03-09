using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanelManager : MonoBehaviour
{
    // 假设有一个音量滑块
    public Slider volumeSlider;
    const string VolumeKey = "MasterVolume";

    void Start()
    {
        // 加载已保存的音量设置
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey);
        volumeSlider.value = savedVolume;
        // 更新音频系统...
    }

    private void Update()
    {
        OnVolumeChanged(volumeSlider);
    }

    public void OnVolumeChanged(Slider slider)
    {
        // 当音量滑块值改变时，保存新的音量值
        float newVolume = slider.value;
        // 更新音频系统...

        PlayerPrefs.SetFloat(VolumeKey, newVolume);
        PlayerPrefs.Save(); // 立即保存到PlayerPrefs
    }
}
