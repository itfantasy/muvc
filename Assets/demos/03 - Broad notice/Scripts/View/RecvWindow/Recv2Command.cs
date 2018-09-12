using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using itfantasy.umvc;

class Recv2Command : Command
{
    public const int Recv2Command_Index = Worker_Cloud.CommandIndex + 300;
    public const int Recv2Command_Show = Recv2Command_Index + 1;

    public override void Execute(Notice notice)
    {
        switch(notice.code)
        {
            case Recv2Command_Show:
                GameObject root = GameObject.Find("UIRoot");
                RegisterMediator<RecvMediator>(root.transform.Find("Recv2Window").gameObject);
                break;
            case BroadCommand.BroadCommand_AddValue:
                SendNoticeToMediator(notice);
                break;
        }
        base.Execute(notice);
    }
}

