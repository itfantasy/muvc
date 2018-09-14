using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using itfantasy.umvc;

public class MainMediator : Mediator
{
    MainView view;
    MainCommand cmd
    {
        get
        {
            return this._command as MainCommand;
        }
    }

    protected override void OnInitialize()
    {
        this.view = AttachView<MainView>();
        base.OnInitialize();
    }

    protected override void SetEventListener()
    {
        EventTriggerListener.Get(this.view.button.gameObject).onClick = this.OnClick;
        base.SetEventListener();
    }

    protected override void OnClick(GameObject go)
    {
        this.cmd.AsyncMainToSub((token) =>
        {
            this.view.text.text = token.ToString();
        }, this.view.input.text);
        base.OnClick(go);
    }

    public override void HandleNotice(INotice notice)
    {
        base.HandleNotice(notice);
    }

}

