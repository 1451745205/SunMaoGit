using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ModelClose : MonoBehaviour
{
    public GameObject[] models;

    void Update()
    {
        // 使用射线检测与鼠标交互
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // 如果射线击中了物体
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                // 遍历数组查找被点击的模型
                foreach (GameObject model in models)
                {
                    if (model == hit.transform.gameObject)
                    {
                        // 点击到模型时，销毁该模型
                        model.SetActive(false);
                        break; // 找到并销毁后跳出循环
                    }
                }
            }
        }
    }
}
