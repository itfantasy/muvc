using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

/// <summary>
/// 描述：
/// 作者： 
/// </summary>
public class Mediator1 : Mediator {

    View1 view;
    Command1 cmd
    {
        get
        {
            return this._command as Command1;
        }
    }

    protected override void OnInitialize()
    {
        this.view = this.AttachView<View1>();
        base.OnInitialize();
    }

    protected override void SetEventListener()
    {
        EventTriggerListener.Get(this.view.button.gameObject).onClick = this.OnClick;
        base.SetEventListener();
    }

    protected override void OnClick(GameObject go)
    {
        this.cmd.TransValueToScene2(this.view.input.text);
        base.OnClick(go);
    }

    public override void HandleNotice(INotice notice)
    {
        switch(notice.GetType())
        {
            case Command1.Command1_OK:
                this.view.input.text = notice.GetBody()[0].ToString();
                break;
        }
        base.HandleNotice(notice);
    }
}
