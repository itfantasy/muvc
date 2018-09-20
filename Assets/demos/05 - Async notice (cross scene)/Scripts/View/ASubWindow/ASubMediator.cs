using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

/// <summary>
/// 描述：
/// 作者： 
/// </summary>
public class ASubMediator : Mediator {

    SubView view;
    ASubCommand cmd
    {
        get
        {
            return this._command as ASubCommand;
        }
    }

    INotice _notice;

    protected override void OnInitialize()
    {
        this.view = AttachView<SubView>();
        base.OnInitialize();
    }

    protected override void SetEventListener()
    {
        EventTriggerListener.Get(this.view.button.gameObject).onClick = this.OnClick;
        base.SetEventListener();
    }

    protected override void OnClick(GameObject go)
    {
        this.cmd.FinishNotice(this._notice);
        base.OnClick(go);
    }

    public override void HandleNotice(INotice notice)
    {
        switch (notice.GetType())
        {
            case ASubCommand.Command_OK:
                this.view.text.text = notice.GetToken().ToString();
                this._notice = notice;
                break;
        }
        base.HandleNotice(notice);
    }
}
