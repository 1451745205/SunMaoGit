using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XFramework;
using XFramework.Extend;
using UnityEngine.Audio;
using UnityEditor;

/// <summary>
/// 设置面板
/// </summary>
public class SettingPanel : BasePanel
{
    /// <summary>
    /// 路径
    /// </summary>
    static readonly string path = "Prefabs/UI/Panel/SettingPanel";
    AudioMixer mixer;


    /// <summary>
    /// 设置面板
    /// </summary>
    public SettingPanel() : base(new UIType(path))
    {
        mixer = Resources.Load<AudioMixer>("AudioMixer/AudioMixer");
    }

    protected override void InitEvent()
    {
        Slider bgmSlider = ActivePanel.GetOrAddComponentInChildren<Slider>("BGMSlider");
        Slider sfxSlider = ActivePanel.GetOrAddComponentInChildren<Slider>("SFXSlider");

        float bgmValue, sfxValue;
        mixer.GetFloat("BGM", out bgmValue);
        mixer.GetFloat("SFX", out sfxValue);
        bgmSlider.value = bgmValue;
        sfxSlider.value = sfxValue;

        bgmSlider.onValueChanged.AddListener(value =>
        {
            //lambda表达式
            mixer.SetFloat("BGM", value);
            if (value == bgmSlider.minValue)
            {
                mixer.SetFloat("BGM", -80f);
            }
        });
        sfxSlider.onValueChanged.AddListener(value =>
        {
            //lambda表达式
            mixer.SetFloat("SFX", value);
            if (value == sfxSlider.minValue)
            {
                mixer.SetFloat("SFX", -80f);
            }
        });

        ActivePanel.GetOrAddComponentInChildren<Button>("BtnExit").onClick.AddListener(() =>
        {
            //播放点击音效
            AudioManager.PlayAudio("AudioName.Click");
            Pop();
        });
    }
}
