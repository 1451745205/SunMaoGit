using UnityEngine;

public class ModelController : MonoBehaviour
{
	public GameObject[] smallmodels;

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
	}
	void OnMouseDrag()     //鼠标拖拽时的操作// 
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
		//这个是是按照之前方向一直慢速旋转
		this.transform.Rotate(new Vector3(axisY, axisX, 0) * Rigid(), Space.World);

		//执行隐藏小方块模型的方法
		CloseModel();
	}

	/// <summary>
	/// 隐藏小方块模型
	/// </summary>
	public void CloseModel()
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
				foreach (GameObject model in smallmodels)
				{
					Debug.Log(hit.transform.gameObject.name);
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
