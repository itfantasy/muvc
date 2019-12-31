using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

/// <summary>
/// 描述：
/// 作者： 
/// </summary>
public class PreBMediator : Mediator {

    Button button;

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
        SendNotice(Command.Command_Close);
        base.OnClick(go);
    }
}
