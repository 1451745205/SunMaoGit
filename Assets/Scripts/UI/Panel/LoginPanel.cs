using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XFramework;
using XFramework.Extend;

public class LoginPanel : BasePanel
{
    /// <summary>
    /// Â·¾¶
    /// </summary>
    static readonly string path = "Prefabs/UI/Panel/LoginPanel";

    /// <summary>
    /// Ö÷Ãæ°å
    /// </summary>
    public LoginPanel() : base(new UIType(path))
    {

    }

    //public UserManager userManager;

    protected override void InitEvent()
    {
        ActivePanel.GetOrAddComponentInChildren<Button>("BtnLogin").onClick.AddListener(() =>
        {
            if (UserManager.myUsername !=null)
            {
                Pop();
            }
        });
    }
}
