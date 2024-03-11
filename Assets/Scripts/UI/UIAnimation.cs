using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimation : MonoBehaviour
{
    public Animator animator;
    private bool isEnter = false;

    //������ʱ��ʾ��Ʒ��Ϣ
    public void OnMouseEnter()
    {
        isEnter = true;
        animator.SetBool("isEnter", isEnter);
        Debug.Log("�����ͣ");
        Debug.Log(isEnter);
    }
    public void OnMouseExit()
    {
        isEnter = false;
        animator.SetBool("isEnter", isEnter);
        Debug.Log("����˳�");
    }
}
