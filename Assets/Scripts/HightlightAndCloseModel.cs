using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HightlightAndCloseModel : MonoBehaviour
{
	Renderer currentRenderer; // ��ǰѡ����Renderer
	Material[] originalMaterials; // ��ǰѡ����Rendererԭʼ�Ĳ���������

	private static HightlightAndCloseModel instance;
	public static HightlightAndCloseModel Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<HightlightAndCloseModel>();
				if (instance == null)
				{
					Debug.LogError("HighlightAndCloseModel script missing on a GameObject in the scene.");
				}
			}
			return instance;
		}
	}

	/// <summary>
	/// ģ�͸��� �� ����С����ģ��
	/// </summary>
	public void HightlightAndClose()
	{
		//��ȡ����������
		string path = "Models/Materials/HightLight";
		Material secondMaterial = Resources.Load<Material>(path); // �ڶ���������

		// ʹ�����߼������꽻��
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			LayerMask layerMask = LayerMask.GetMask("SmallModel");

			// ������߻���������
			if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
			{
				Renderer hitRenderer = hit.transform.gameObject.GetComponent<Renderer>();

				if (hitRenderer != currentRenderer)
				{
					// �Ƴ���һ��ѡ��ģ�͵ĵڶ���������
					if (currentRenderer != null && originalMaterials != null)
					{
						currentRenderer.materials = originalMaterials;
					}

					// ��¼��ǰѡ����ģ�ͺ���ԭʼ������
					currentRenderer = hitRenderer;
					originalMaterials = hitRenderer.materials;

					// ��ӵڶ��������򵽵�ǰѡ����ģ��
					Material[] newMaterials = new Material[originalMaterials.Length + 1];
					originalMaterials.CopyTo(newMaterials, 0);
					newMaterials[originalMaterials.Length] = secondMaterial;
					currentRenderer.materials = newMaterials;
				}
				else
				{
					// ����ٴε��ͬһģ�ͣ������ظ�ģ��
					hit.transform.gameObject.SetActive(false);
					currentRenderer = null;
					originalMaterials = null;
				}
			}
		}
	}
}
