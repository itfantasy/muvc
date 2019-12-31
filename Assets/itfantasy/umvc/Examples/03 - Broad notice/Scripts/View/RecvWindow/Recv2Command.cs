using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using itfantasy.umvc;

class Recv2Command : Command
{
    public const int Index = Worker_Cloud.Index + 300;
    public const int Recv2Command_Show = Index + 1;

    public override void Execute(INotice notice)
    {
        switch(notice.GetType())
        {
            case Recv2Command_Show:
                GameObject root = GameObject.Find("UIRoot");
                RegisterMediator<RecvMediator>(root.transform.Find("Recv2Window").gameObject);
                break;
            case Worker_Cloud.Broad_AddValue:
                SendToMediator(notice);
                break;
        }
        base.Execute(notice);
    }
}

