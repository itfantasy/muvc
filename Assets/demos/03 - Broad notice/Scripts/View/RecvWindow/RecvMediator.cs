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

    public override void HandleNotice(INotice notice)
    {
        switch(notice.GetType())
        {
            case Worker_Cloud.Broad_AddValue:
                this.view.num += (int)notice.GetBody()[0];
                break;
        }
        base.HandleNotice(notice);
    }
}

