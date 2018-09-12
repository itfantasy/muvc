using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using itfantasy.umvc;

public class MainMediator : Mediator
{
    MainView view;

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
        SendNoticeToCommand(new MainToSubVo(MainCommand.MainCommand_OK, (token) =>
        {
            this.view.text.text = token.ToString();
        }, this.view.input.text));
        base.OnClick(go);
    }

    public override void HandleNotice(Notice notice)
    {
        base.HandleNotice(notice);
    }

}

