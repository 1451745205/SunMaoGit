using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ModelClose : MonoBehaviour
{
    public GameObject[] models;

    void Update()
    {
        // ʹ�����߼������꽻��
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // ������߻���������
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                // ����������ұ������ģ��
                foreach (GameObject model in models)
                {
                    if (model == hit.transform.gameObject)
                    {
                        // �����ģ��ʱ�����ٸ�ģ��
                        model.SetActive(false);
                        break; // �ҵ������ٺ�����ѭ��
                    }
                }
            }
        }
    }
}
