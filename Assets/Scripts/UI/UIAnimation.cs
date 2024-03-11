using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimation : MonoBehaviour
{
    public Animator animator;
    private bool isEnter = false;

    //鼠标进入时显示物品信息
    public void OnMouseEnter()
    {
        isEnter = true;
        animator.SetBool("isEnter", isEnter);
        Debug.Log("鼠标悬停");
        Debug.Log(isEnter);
    }
    public void OnMouseExit()
    {
        isEnter = false;
        animator.SetBool("isEnter", isEnter);
        Debug.Log("鼠标退出");
    }
}
