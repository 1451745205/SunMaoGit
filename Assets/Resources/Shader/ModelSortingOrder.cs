using UnityEngine;

public class ModelSortingOrder : MonoBehaviour
{
    // 定义一个变量来保存模型的初始渲染层级
    private int originalSortingOrder;

    // 定义一个变量来保存 UI 渲染层级的索引值
    public int uiSortingOrder = 0;

    // 在启动时保存模型的初始渲染层级
    void Start()
    {
        // 获取模型的渲染层级
        Renderer renderer = GetComponent<Renderer>();
        originalSortingOrder = renderer.sortingOrder;
    }

    // 绘制 Gizmos
    void OnDrawGizmos()
    {
        // 获取模型的渲染器组件
        Renderer renderer = GetComponent<Renderer>();

        // 设置 Gizmos 的颜色
        Gizmos.color = Color.red;

        // 绘制模型的边界框
        Gizmos.DrawWireCube(renderer.bounds.center, renderer.bounds.size);
    }

    // 设置模型的渲染层级为 UI 渲染层级的索引值
    public void SetModelSortingOrderToUI()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.sortingOrder = uiSortingOrder;
    }

    // 恢复模型的渲染层级为初始值
    public void RestoreModelSortingOrder()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.sortingOrder = originalSortingOrder;
    }
}
