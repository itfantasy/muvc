using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using itfantasy.umvc;

class Recv1Command : Command
{
    public const int Index = Worker_Cloud.Index + 200;
    public const int Recv1Command_Show = Index + 1;

    public override void Execute(INotice notice)
    {
        switch(notice.GetType())
        {
            case Recv1Command_Show:
                GameObject root = GameObject.Find("UIRoot");
                RegisterMediator<RecvMediator>(root.transform.Find("Recv1Window").gameObject);
                break;
            case Worker_Cloud.Broad_AddValue:
                SendToMediator(notice);
                break;
        }
        base.Execute(notice);
    }
}

