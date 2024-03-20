using System.Collections;
using UnityEngine;

public class ExchangePos : MonoBehaviour
{
    public GameObject[] bigModels;  // �î����ģ��
    public float duration = 2f; // ���ų���ʱ��
    public Vector3 targetExScale01 = new Vector3(0.5f, 0.5f, 0.5f); // Ŀ�����Ŵ�С01
    public Vector3 targetExScale02 = Vector3.one; // Ŀ�����Ŵ�С02

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

    private void Update()
    {

        //ִ��ģ�͸��� �� ����С����ģ�͵ķ���
        HightlightAndCloseModel.Instance.HightlightAndClose();
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

            obj1.transform.localScale = Vector3.Lerp(targetExScale02, targetExScale01, currentTime / duration);
            obj2.transform.localScale = Vector3.Lerp(targetExScale01, targetExScale02, currentTime / duration);

            currentTime += Time.deltaTime;
            yield return null;
        }

        obj1.transform.localPosition = hitPosObj1;
        obj2.transform.localPosition = hitPosObj2;

        obj1.transform.localScale = targetExScale01;
        obj2.transform.localScale = targetExScale02;
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

    /// <summary>
    /// ���Ŷ���
    /// </summary>
    private void PlayAnimation(bool isVictory)
    {
        Animator animator = GetComponent<Animator>();
        animator.SetBool("isVictory", isVictory); // �����Ƿ�ʤ��������ʤ����������
    }

    /// <summary>
    /// ƥ�����ж�
    /// </summary>
    public void JudgeResult()
    {

        bool allTrue = true; // ��������������Ԫ�ض�Ϊ��

        for (int i = 0; i < bigModels.Length; i++)
        {
            // ��ȡ �󲿼����ص� ModelController ���ʵ��
            ModelController modelController = bigModels[i].GetComponent<ModelController>();
            GameObject[] models = modelController.smallmodels;

            //����󲿼�������Ҫ�жϵ�ģ�͵Ļ�
            if (models.Length != 0)
            {
                for (int j = 0; j < models.Length; j++)
                {
                    //����������мٵģ����˳�ѭ�����ж���Ϸʧ��
                    if (models[j].activeSelf)
                    {
                        Debug.Log("ƥ��ʧ�ܣ�");
                        allTrue = false; // ���������Ϊ��
                        PlayAnimation(false);  //����ʧ�ܶ���
                        break; // �˳�ѭ��
                    }
                }
                if (allTrue)
                {
                    Debug.Log("ƥ��ɹ���");
                    PlayAnimation(true);  //���ųɹ�����
                }
            }
        }
    }



    /// <summary>
    /// ģ��ƥ�䣨��ģ��ת����Ŀ�궯��λ�ã�
    /// </summary>
    public Vector3[] targetPartPosition;
    public Vector3[] targetPartRotation;
    public Vector3[] targetPartScale;

    private float moveSpeed = 0.5f; // �ƶ��ٶ�
    private float rotateSpeed = 180f; // ��ת�ٶ�
    private float scaleSpeed = 2.5f; // �����ٶ�

    private bool isMoving = false;

    public void MoveAndRotateToTarget()
    {
        if (!isMoving)
        {
            StartCoroutine(TransModels());
        }
    }
    /// <summary>
    /// �任ȫ��ģ��
    /// </summary>
    /// <returns></returns>
    private IEnumerator TransModels()
    {
        int minArrayLength = Mathf.Min(bigModels.Length, targetPartPosition.Length, targetPartRotation.Length, targetPartScale.Length);

        for (int i = 0; i < minArrayLength; i++)
        {
            yield return TransModel(bigModels[i], targetPartPosition[i], targetPartRotation[i], targetPartScale[i]);
        }
    }
    /// <summary>
    /// �任һ��ģ��
    /// </summary>
    /// <param name="model"></param>
    /// <param name="targetPosition"></param>
    /// <param name="targetScale"></param>
    /// <returns></returns>
    private IEnumerator TransModel(GameObject model, Vector3 targetPosition, Vector3 targetRotation, Vector3 targetScale)
    {
        isMoving = true;

        float distanceThreshold = 0.001f;  // �ƶ��������ֵ
        float angleThreshold = 0.001f;  // ��ת�Ƕȵ���ֵ

        while (Vector3.Distance(model.transform.position, targetPosition) > distanceThreshold)
        {
            model.transform.position = Vector3.MoveTowards(model.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        while (Quaternion.Angle(model.transform.rotation, Quaternion.Euler(targetRotation)) > angleThreshold)
        {
            model.transform.rotation = Quaternion.RotateTowards(model.transform.rotation, Quaternion.Euler(targetRotation), rotateSpeed * Time.deltaTime);
            yield return null;
        }

        while (Vector3.Distance(model.transform.localScale, targetScale) > distanceThreshold)
        {
            model.transform.localScale = Vector3.MoveTowards(model.transform.localScale, targetScale, scaleSpeed * Time.deltaTime);
            yield return null;
        }
        isMoving = false;
    }
}