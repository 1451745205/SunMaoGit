using UnityEngine;

public class ModelController : MonoBehaviour
{
	public GameObject[] smallmodels;  //���Ա������Сģ��

	public Material secondMaterial; // �ڶ���������

	Renderer currentRenderer; // ��ǰѡ����Renderer
	Material[] originalMaterials; // ��ǰѡ����Rendererԭʼ�Ĳ���������


	private bool onDrag = false;  //�Ƿ���ק    
	public float speed = 6f;      //��ת�ٶ�    
	private float tempSpeed;      //�����ٶ� 
	private float axisX = 1;      //�����ˮƽ�����ƶ�������   
	private float axisY = 1;      //�������ֱ�����ƶ�������   
	private float cXY;

	void OnMouseDown()
	{
		//������갴�µ��¼�// 
		axisX = 0f; axisY = 0f;

		//����ģ�Ͳ���(λ��,��С)
		ExchangePos.Instance.ExModel(this.gameObject);

	}

	//�����קʱ�Ĳ���
	void OnMouseDrag()     
	{
		onDrag = true;
		axisX = -Input.GetAxis("Mouse X");
		//���������� 
		axisY = Input.GetAxis("Mouse Y");
		cXY = Mathf.Sqrt(axisX * axisX + axisY * axisY); //��������ƶ��ĳ���//
		if (cXY == 0f) { cXY = 1f; }
	}

	//���������ٶ�
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
				//ͨ����������ƶ�����ʵ����קԽ���ٶȼ���Խ��
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
		//�ж��Ƿ���ק���ǵĻ���ʼ��ת
		if (onDrag)
		{
			this.transform.Rotate(new Vector3(axisY, axisX, 0) * Rigid(), Space.World);
		}
		else
		{
			tempSpeed = 0f;
		}

		//ִ��ģ�͸���������С����ģ�͵ķ���
		HightlightAndCloseModel();
	}

	void OnMouseUp()
	{
		onDrag = false;
	}

	/// <summary>
	/// ģ�͸��� �� ����С����ģ��
	/// </summary>
	public void HightlightAndCloseModel()
	{
		// ʹ�����߼������꽻��
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			LayerMask layerMask = LayerMask.GetMask("SmallModel");

			// ������߻���������
			if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
			{
				Debug.Log(hit.transform.gameObject.layer);
				

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

				//// ����������ұ������ģ��
				//foreach (GameObject model in smallmodels)
				//{
				//	if (model == hit.transform.gameObject)
				//	{
				//		Renderer hitRenderer = hit.transform.gameObject.GetComponent<Renderer>();

				//		if (hitRenderer != currentRenderer)
				//		{
				//			// �Ƴ���һ��ѡ��ģ�͵ĵڶ���������
				//			if (currentRenderer != null && originalMaterials != null)
				//			{
				//				currentRenderer.materials = originalMaterials;
				//			}

				//			// ��¼��ǰѡ����ģ�ͺ���ԭʼ������
				//			currentRenderer = hitRenderer;
				//			originalMaterials = hitRenderer.materials;

				//			// ��ӵڶ��������򵽵�ǰѡ����ģ��
				//			Material[] newMaterials = new Material[originalMaterials.Length + 1];
				//			originalMaterials.CopyTo(newMaterials, 0);
				//			newMaterials[originalMaterials.Length] = secondMaterial;

				//			currentRenderer.materials = newMaterials;
				//		}
				//		else
				//		{
				//			// ����ٴε��ͬһģ�ͣ������ظ�ģ��
				//			hit.transform.gameObject.SetActive(false);
				//			currentRenderer = null;
				//			originalMaterials = null;
				//		}

				//		break; // �ҵ������������ѭ��
				//	}
				//}
			}
		}
	}
}
