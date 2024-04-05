using UnityEngine;

public class ModelSortingOrder : MonoBehaviour
{
    // ����һ������������ģ�͵ĳ�ʼ��Ⱦ�㼶
    private int originalSortingOrder;

    // ����һ������������ UI ��Ⱦ�㼶������ֵ
    public int uiSortingOrder = 0;

    // ������ʱ����ģ�͵ĳ�ʼ��Ⱦ�㼶
    void Start()
    {
        // ��ȡģ�͵���Ⱦ�㼶
        Renderer renderer = GetComponent<Renderer>();
        originalSortingOrder = renderer.sortingOrder;
    }

    // ���� Gizmos
    void OnDrawGizmos()
    {
        // ��ȡģ�͵���Ⱦ�����
        Renderer renderer = GetComponent<Renderer>();

        // ���� Gizmos ����ɫ
        Gizmos.color = Color.red;

        // ����ģ�͵ı߽��
        Gizmos.DrawWireCube(renderer.bounds.center, renderer.bounds.size);
    }

    // ����ģ�͵���Ⱦ�㼶Ϊ UI ��Ⱦ�㼶������ֵ
    public void SetModelSortingOrderToUI()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.sortingOrder = uiSortingOrder;
    }

    // �ָ�ģ�͵���Ⱦ�㼶Ϊ��ʼֵ
    public void RestoreModelSortingOrder()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.sortingOrder = originalSortingOrder;
    }
}
