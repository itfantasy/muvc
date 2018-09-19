using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

/// <summary>
/// 描述：
/// 作者： 
/// </summary>
public class ComMediator : Mediator {

    public Button button;
    ComCommand cmd
    {
        get
        {
            return this._command as ComCommand;
        }
    }

    protected override void OnInitialize()
    {
        this.button = this.transform.Find("Image/Button").GetComponent<Button>();
        base.OnInitialize();
    }

    protected override void SetEventListener()
    {
        EventTriggerListener.Get(this.button.gameObject).onClick = this.OnClick;
        base.SetEventListener();
    }

    protected override void OnClick(GameObject go)
    {
        this.cmd.ShowTweenWindow();
        base.OnClick(go);
    }
	
}
