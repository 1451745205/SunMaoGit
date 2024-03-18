using UnityEngine;

public class ModelController : MonoBehaviour
{
	public GameObject[] smallmodels;  //可以被点击的小模型

	public Material secondMaterial; // 第二个材质球

	Renderer currentRenderer; // 当前选定的Renderer
	Material[] originalMaterials; // 当前选定的Renderer原始的材质球数组


	private bool onDrag = false;  //是否被拖拽    
	public float speed = 6f;      //旋转速度    
	private float tempSpeed;      //阻尼速度 
	private float axisX = 1;      //鼠标沿水平方向移动的增量   
	private float axisY = 1;      //鼠标沿竖直方向移动的增量   
	private float cXY;

	void OnMouseDown()
	{
		//接受鼠标按下的事件// 
		axisX = 0f; axisY = 0f;

		//交换模型参数(位置,大小)
		ExchangePos.Instance.ExModel(this.gameObject);

	}

	//鼠标拖拽时的操作
	void OnMouseDrag()     
	{
		onDrag = true;
		axisX = -Input.GetAxis("Mouse X");
		//获得鼠标增量 
		axisY = Input.GetAxis("Mouse Y");
		cXY = Mathf.Sqrt(axisX * axisX + axisY * axisY); //计算鼠标移动的长度//
		if (cXY == 0f) { cXY = 1f; }
	}

	//计算阻尼速度
	float Rigid()
	{
		if (onDrag)
		{
			tempSpeed = speed;
		}
		else
		{
			if (tempSpeed > 0)
			{
				//通过除以鼠标移动长度实现拖拽越长速度减缓越慢
				tempSpeed -= speed * 2 * Time.deltaTime / cXY;
			}
			else
			{
				tempSpeed = 0;
			}
		}
		return tempSpeed;
	}

	void Update()
	{
		//判断是否拖拽，是的话开始旋转
		if (onDrag)
		{
			this.transform.Rotate(new Vector3(axisY, axisX, 0) * Rigid(), Space.World);
		}
		else
		{
			tempSpeed = 0f;
		}

		//执行模型高亮及隐藏小方块模型的方法
		HightlightAndCloseModel();
	}

	void OnMouseUp()
	{
		onDrag = false;
	}

	/// <summary>
	/// 模型高亮 及 隐藏小方块模型
	/// </summary>
	public void HightlightAndCloseModel()
	{
		// 使用射线检测与鼠标交互
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			LayerMask layerMask = LayerMask.GetMask("SmallModel");

			// 如果射线击中了物体
			if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
			{
				Debug.Log(hit.transform.gameObject.layer);
				

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

				//// 遍历数组查找被点击的模型
				//foreach (GameObject model in smallmodels)
				//{
				//	if (model == hit.transform.gameObject)
				//	{
				//		Renderer hitRenderer = hit.transform.gameObject.GetComponent<Renderer>();

				//		if (hitRenderer != currentRenderer)
				//		{
				//			// 移除上一个选定模型的第二个材质球
				//			if (currentRenderer != null && originalMaterials != null)
				//			{
				//				currentRenderer.materials = originalMaterials;
				//			}

				//			// 记录当前选定的模型和其原始材质球
				//			currentRenderer = hitRenderer;
				//			originalMaterials = hitRenderer.materials;

				//			// 添加第二个材质球到当前选定的模型
				//			Material[] newMaterials = new Material[originalMaterials.Length + 1];
				//			originalMaterials.CopyTo(newMaterials, 0);
				//			newMaterials[originalMaterials.Length] = secondMaterial;

				//			currentRenderer.materials = newMaterials;
				//		}
				//		else
				//		{
				//			// 如果再次点击同一模型，则隐藏该模型
				//			hit.transform.gameObject.SetActive(false);
				//			currentRenderer = null;
				//			originalMaterials = null;
				//		}

				//		break; // 找到并处理后跳出循环
				//	}
				//}
			}
		}
	}
}
