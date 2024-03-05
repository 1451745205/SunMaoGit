using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������������ջ���洢UI
/// </summary>
public class PanelManager
{
    /// <summary>
    /// �洢UI����ջ
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
    /// UI����ջ�������˲�������ʾһ�����
    /// </summary>
    /// <param name="nextPanel">Ҫ��ʾ�����</param>
    public void Push(BasePanel nextPanel)
    {
        //��ʾ��ҳ��֮ǰҪ��ͣ��һ�����
        if(stackPanel.Count > 0)  //���������������
        {
            panel = stackPanel.Peek();   //��ȡջ��
            panel.OnPause();   //��ͣ
        }
        stackPanel.Push(nextPanel);   //��ջ
        GameObject panelGo = uiManager.GetSingleUI(nextPanel.UIType);   //ʵ�������
        nextPanel.Initialize(new UITool(panelGo));   //ʵ����UITool
        nextPanel.Initialize(this);  //ʵ������������
        nextPanel.Initialize(uiManager);  //ʵ����UI������
        nextPanel.OnEnter();
    }

    /// <summary>
    /// ִ�����ĳ�ջ�������˲�����ִ������OnExit��OnResume����
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
