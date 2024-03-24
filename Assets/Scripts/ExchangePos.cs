using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;

public class ExchangePos : MonoBehaviour
{
    public GameObject[] bigModels;  // 榫卯部件模型
    public float duration = 2f; // 缩放持续时间
    public Vector3 targetExScale01 = new Vector3(0.5f, 0.5f, 0.5f); // 目标缩放大小01
    public Vector3 targetExScale02 = Vector3.one; // 目标缩放大小02

    public int currentLevelIndex = int.Parse(SelectLevelPanel.levelName);  // 当前是第几关
    private int unlockedLevel = 0;  // 已解锁的关卡数量

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

        //执行模型高亮 及 隐藏小方块模型的方法
        HightlightAndCloseModel.Instance.HightlightAndClose();
    }

    /// <summary>
    /// 交换碰撞体状态，交换位置和大小
    /// </summary>
    /// <param name="gameObject"></param>
    public void ExModel(GameObject gameObject)
    {
        if (gameObject.transform.localPosition.x !=0 || gameObject.transform.localPosition.y != 0)
        {
            // 关闭子物体的碰撞器
            DisableAllColliders(gameObject,false);
            for (int i = 0; i < bigModels.Length; i++)
            {
                if (bigModels[i].transform.localPosition.x == 0 && bigModels[i].transform.localPosition.y == 0)
                {
                    // 交换位置和大小
                    StartCoroutine(MoveAndScale(bigModels[i], gameObject));
                }
            }
        }
        else
        {
            // 开启子物体的碰撞器
            DisableAllColliders(gameObject, true);
        }
    }

    /// <summary>
    /// 交换位置和大小
    /// </summary>
    /// <param name="obj1"></param>
    /// <param name="obj2"></param>
    /// <returns></returns>
    private IEnumerator MoveAndScale(GameObject obj1, GameObject obj2)
    {
        Vector3 originalPosObj1 = obj1.transform.localPosition;
        Vector3 originalPosObj2 = obj2.transform.localPosition;

        Vector3 hitPosObj1 = obj2.transform.localPosition;
        hitPosObj1.z = originalPosObj1.z; // 保持 obj1 的 Z 轴值不变

        Vector3 hitPosObj2 = obj1.transform.localPosition;
        hitPosObj2.z = originalPosObj2.z; // 保持 obj2 的 Z 轴值不变

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
    /// 设置子物体的碰撞器状态
    /// </summary>
    /// <param name="parentTransform">父物体</param>
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
    /// 播放动画
    /// </summary>
    private void PlayAnimation(int clipNumber)
    {
        AnimationClip[] animationClips = AnimationUtility.GetAnimationClips(transform.gameObject);
        Animation animation = GetComponent<Animation>();
        animation.Play(animationClips[clipNumber].name);
    }


    /// <summary>
    /// 匹配结果判定
    /// </summary>
    public void JudgeResult()
    {
        bool allTrue = true; // 假设数组中所有元素都为真

        for (int i = 0; i < bigModels.Length; i++)
        {
            // 获取 大部件挂载的 ModelController 类的实例
            ModelController modelController = bigModels[i].GetComponent<ModelController>();
            GameObject[] models = modelController.smallmodels;

            //如果大部件下有需要判断的模型的话
            if (models.Length != 0)
            {
                for (int j = 0; j < models.Length; j++)
                {
                    //如果数组中有假的，则退出循环，判断游戏失败
                    if (models[j].activeSelf)
                    {
                        Debug.Log("匹配失败！");
                        allTrue = false; // 将标记设置为假
                        PlayAnimation(0);  //播放失败动画
                        break; // 退出循环
                    }
                }
                if (allTrue)
                {
                    Debug.Log("匹配成功！");
                    if(currentLevelIndex > unlockedLevel )
                    {

                    }
                    PlayAnimation(1);  //播放成功动画
                }
            }
        }
    }



    /// <summary>
    /// 模型匹配（将模型转换到目标动画位置）
    /// </summary>
    public Vector3[] targetPartPosition;
    public Vector3[] targetPartRotation;
    public Vector3[] targetPartScale;

    private float moveSpeed = 0.5f; // 移动速度
    private float rotateSpeed = 180f; // 旋转速度
    private float scaleSpeed = 2.5f; // 缩放速度

    private bool isMoving = false;

    public void MoveAndRotateToTarget()
    {
        if (!isMoving)
        {
            StartCoroutine(TransModels());
        }
    }
    /// <summary>
    /// 变换全部模型
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
    /// 变换一个模型
    /// </summary>
    /// <param name="model"></param>
    /// <param name="targetPosition"></param>
    /// <param name="targetScale"></param>
    /// <returns></returns>
    private IEnumerator TransModel(GameObject model, Vector3 targetPosition, Vector3 targetRotation, Vector3 targetScale)
    {
        isMoving = true;

        float distanceThreshold = 0.001f;  // 移动距离的阈值
        float angleThreshold = 0.001f;  // 旋转角度的阈值

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