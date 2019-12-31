using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using itfantasy.umvc;

public class SubMediator : Mediator
{
    SubView view;
    SubCommand cmd
    {
        get
        {
            return this._command as SubCommand;
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
        switch(notice.GetType())
        {
            case SubCommand.SubCommand_OK:
                this.view.text.text = notice.token.ToString();
                this._notice = notice;
                break;
        }
        base.HandleNotice(notice);
    }

}

