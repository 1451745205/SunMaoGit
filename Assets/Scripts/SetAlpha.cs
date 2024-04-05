using UnityEngine;
using UnityEngine.UI;

public class SetAlpha : MonoBehaviour
{
    [Range(0f, 1f)]
    public float alphaValue = 1f; // ͸����ֵ����Χ��0����ȫ͸������1����ȫ��͸����

    private void Start()
    {
        // ����͸����
        SetTransparency();
    }

    private void Update()
    {
        // ����ģʽ��ʵʱ����͸����
        if (!Application.isPlaying)
        {
            SetTransparency();
        }
    }

    // ����͸���ȵķ���
    private void SetTransparency()
    {
        // ��ȡ�����������UI���
        Graphic[] graphics = GetComponentsInChildren<Graphic>();

        // ���������壬����͸����
        foreach (Graphic graphic in graphics)
        {
            Color color = graphic.color;
            color.a = alphaValue;
            graphic.color = color;
        }
    }
}
