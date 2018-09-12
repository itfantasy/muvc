using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using itfantasy.umvc;

public class BroadMediator : Mediator
{
    BroadView view;

    protected override void OnInitialize()
    {
        this.view = AttachView<BroadView>();
        base.OnInitialize();
    }

    protected override void SetEventListener()
    {
        EventTriggerListener.Get(this.view.button.gameObject).onClick = this.OnClick;
        base.SetEventListener();
    }

    protected override void OnClick(GameObject go)
    {
        SendNoticeToCommand(new QuickNotice(BroadCommand.BroadCommand_AddValue, 10));
        base.OnClick(go);
    }
}

