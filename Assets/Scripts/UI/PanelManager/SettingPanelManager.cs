using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanelManager : MonoBehaviour
{
    // ������һ����������
    public Slider volumeSlider;
    const string VolumeKey = "MasterVolume";

    void Start()
    {
        // �����ѱ������������
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey);
        volumeSlider.value = savedVolume;
        // ������Ƶϵͳ...
    }

    private void Update()
    {
        OnVolumeChanged(volumeSlider);
    }

    public void OnVolumeChanged(Slider slider)
    {
        // ����������ֵ�ı�ʱ�������µ�����ֵ
        float newVolume = slider.value;
        // ������Ƶϵͳ...

        PlayerPrefs.SetFloat(VolumeKey, newVolume);
        PlayerPrefs.Save(); // �������浽PlayerPrefs
    }
}
