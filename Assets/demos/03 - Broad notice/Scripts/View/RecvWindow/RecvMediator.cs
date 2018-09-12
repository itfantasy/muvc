using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using itfantasy.umvc;

class RecvMediator : Mediator
{
    RecvView view;

    protected override void OnInitialize()
    {
        this.view = AttachView<RecvView>();
        base.OnInitialize();
    }

    public override void HandleNotice(Notice notice)
    {
        switch(notice.code)
        {
            case BroadCommand.BroadCommand_AddValue:
                QuickNotice quick = notice as QuickNotice;
                this.view.num += (int)quick.paramArray[0];
                break;
        }
        base.HandleNotice(notice);
    }
}

