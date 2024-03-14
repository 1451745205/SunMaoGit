using System.Collections;
using UnityEngine;

public class ExchangePos : MonoBehaviour
{
    public GameObject[] bigModels;  // �î����ģ��
    public float duration = 2f; // ���ų���ʱ��
    public Vector3 targetScale01 = new Vector3(0.5f, 0.5f, 0.5f); // Ŀ�����Ŵ�С01
    public Vector3 targetScale02 = Vector3.one; // Ŀ�����Ŵ�С02

    private static ExchangePos instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static ExchangePos Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject singletonObject = new GameObject();
                instance = singletonObject.AddComponent<ExchangePos>();
                singletonObject.name = typeof(ExchangePos).ToString() + " (Singleton)";

                DontDestroyOnLoad(singletonObject);
            }
            return instance;
        }
    }

    /// <summary>
    /// ����ģ�Ͳ���
    /// </summary>
    /// <param name="gameObject"></param>
    public void ExModel(GameObject gameObject)
    {
        if (gameObject.transform.localPosition.x !=0 || gameObject.transform.localPosition.y != 0)
        {
            // �ر����������ײ��
            DisableAllColliders(gameObject,false);
            for (int i = 0; i < bigModels.Length; i++)
            {
                if (bigModels[i].transform.localPosition.x == 0 && bigModels[i].transform.localPosition.y == 0)
                {
                    // ����λ�úʹ�С
                    StartCoroutine(MoveAndScale(bigModels[i], gameObject));
                }
            }
        }
        else
        {
            // �������������ײ��
            DisableAllColliders(gameObject, true);
        }
    }

    /// <summary>
    /// ����λ�úʹ�С
    /// </summary>
    /// <param name="obj1"></param>
    /// <param name="obj2"></param>
    /// <returns></returns>
    private IEnumerator MoveAndScale(GameObject obj1, GameObject obj2)
    {
        Vector3 originalPosObj1 = obj1.transform.localPosition;
        Vector3 originalPosObj2 = obj2.transform.localPosition;

        Vector3 hitPosObj1 = obj2.transform.localPosition;
        hitPosObj1.z = originalPosObj1.z; // ���� obj1 �� Z ��ֵ����

        Vector3 hitPosObj2 = obj1.transform.localPosition;
        hitPosObj2.z = originalPosObj2.z; // ���� obj2 �� Z ��ֵ����

        float currentTime = 0f;

        while (currentTime < duration)
        {
            obj1.transform.localPosition = Vector3.Lerp(originalPosObj1, hitPosObj1, currentTime / duration);
            obj2.transform.localPosition = Vector3.Lerp(originalPosObj2, hitPosObj2, currentTime / duration);

            obj1.transform.localScale = Vector3.Lerp(targetScale02, targetScale01, currentTime / duration);
            obj2.transform.localScale = Vector3.Lerp(targetScale01, targetScale02, currentTime / duration);

            currentTime += Time.deltaTime;
            yield return null;
        }

        obj1.transform.localPosition = hitPosObj1;
        obj2.transform.localPosition = hitPosObj2;

        obj1.transform.localScale = targetScale01;
        obj2.transform.localScale = targetScale02;
    }

    /// <summary>
    /// �������������ײ��״̬
    /// </summary>
    /// <param name="parentTransform">������</param>
    private void DisableAllColliders(GameObject gameObject,bool isClose = false)
    {
        foreach (Transform child in gameObject.transform)
        {
            Collider collider = child.GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = isClose;
            }

            DisableAllColliders(child.gameObject);
        }
    }
}