using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HightlightAndCloseModel : MonoBehaviour
{
	Renderer currentRenderer; // 当前选定的Renderer
	Material[] originalMaterials; // 当前选定的Renderer原始的材质球数组

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
	/// 模型高亮 及 隐藏小方块模型
	/// </summary>
	public void HightlightAndClose()
	{
		//获取高亮材质球
		string path = "Models/Materials/HightLight";
		Material secondMaterial = Resources.Load<Material>(path); // 第二个材质球

		// 使用射线检测与鼠标交互
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			LayerMask layerMask = LayerMask.GetMask("SmallModel");

			// 如果射线击中了物体
			if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
			{
				Renderer hitRenderer = hit.transform.gameObject.GetComponent<Renderer>();

				if (hitRenderer != currentRenderer)
				{
					// 移除上一个选定模型的第二个材质球
					if (currentRenderer != null && originalMaterials != null)
					{
						currentRenderer.materials = originalMaterials;
					}

					// 记录当前选定的模型和其原始材质球
					currentRenderer = hitRenderer;
					originalMaterials = hitRenderer.materials;

					// 添加第二个材质球到当前选定的模型
					Material[] newMaterials = new Material[originalMaterials.Length + 1];
					originalMaterials.CopyTo(newMaterials, 0);
					newMaterials[originalMaterials.Length] = secondMaterial;
					currentRenderer.materials = newMaterials;
				}
				else
				{
					// 如果再次点击同一模型，则隐藏该模型
					hit.transform.gameObject.SetActive(false);
					currentRenderer = null;
					originalMaterials = null;
				}
			}
		}
	}
}
