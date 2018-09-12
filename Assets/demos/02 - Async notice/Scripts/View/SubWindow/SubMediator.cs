using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using itfantasy.umvc;

public class SubMediator : Mediator
{
    SubView view;
    MainToSubVo mainToSubVo;

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
        if (this.mainToSubVo != null)
        {
            this.mainToSubVo.Done();
        }
        this.SendNoticeToCommand(new Notice(SubCommand.SubCommand_Close));
        base.OnClick(go);
    }

    public override void HandleNotice(Notice notice)
    {
        switch(notice.code)
        {
            case MainCommand.MainCommand_OK:
                mainToSubVo = notice as MainToSubVo;
                this.view.text.text = mainToSubVo.token.ToString();
                break;
        }
        base.HandleNotice(notice);
    }

}

