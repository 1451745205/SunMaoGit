using UnityEngine;
using UnityEngine.UI;

public class SetAlpha : MonoBehaviour
{
    [Range(0f, 1f)]
    public float alphaValue = 1f; // 透明度值，范围从0（完全透明）到1（完全不透明）

    private void Start()
    {
        // 设置透明度
        SetTransparency();
    }

    private void Update()
    {
        // 调试模式下实时更新透明度
        if (!Application.isPlaying)
        {
            SetTransparency();
        }
    }

    // 设置透明度的方法
    private void SetTransparency()
    {
        // 获取所有子物体的UI组件
        Graphic[] graphics = GetComponentsInChildren<Graphic>();

        // 遍历子物体，设置透明度
        foreach (Graphic graphic in graphics)
        {
            Color color = graphic.color;
            color.a = alphaValue;
            graphic.color = color;
        }
    }
}
