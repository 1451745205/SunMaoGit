using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 面板管理器，用栈来存储UI
/// </summary>
public class PanelManager
{
    /// <summary>
    /// 存储UI面板的栈
    /// </summary>
    private Stack<BasePanel> stackPanel;

    private UIManager uiManager;
    private BasePanel panel;

    public PanelManager()
    {
        stackPanel = new Stack<BasePanel>();
        uiManager = new UIManager();
    }

    /// <summary>
    /// UI的入栈操作，此操作会显示一个面板
    /// </summary>
    /// <param name="nextPanel">要显示的面板</param>
    public void Push(BasePanel nextPanel)
    {
        //显示新页面之前要暂停上一个面板
        if(stackPanel.Count > 0)  //即至少有两个面板
        {
            panel = stackPanel.Peek();   //获取栈顶
            panel.OnPause();   //暂停
        }
        stackPanel.Push(nextPanel);   //入栈
        GameObject panelGo = uiManager.GetSingleUI(nextPanel.UIType);   //实例化面板
        nextPanel.Initialize(new UITool(panelGo));   //实例化UITool
        nextPanel.Initialize(this);  //实例化面板管理器
        nextPanel.Initialize(uiManager);  //实例化UI管理器
        nextPanel.OnEnter();
    }

    /// <summary>
    /// 执行面板的出栈操作，此操作会执行面板的OnExit和OnResume方法
    /// </summary>
    public void Pop()
    {
        if(stackPanel.Count > 0)
        {
            stackPanel.Peek().OnExit();
            stackPanel.Pop();
        }
        if(stackPanel.Count > 0)
        {
            stackPanel.Peek().OnResume();
        }
    }
}
