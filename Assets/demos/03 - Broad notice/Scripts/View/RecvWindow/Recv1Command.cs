using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using itfantasy.umvc;

class Recv1Command : Command
{
    public const int Recv1Command_Index = Worker_Cloud.CommandIndex + 200;
    public const int Recv1Command_Show = Recv1Command_Index + 1;

    public override void Execute(Notice notice)
    {
        switch(notice.code)
        {
            case Recv1Command_Show:
                GameObject root = GameObject.Find("UIRoot");
                RegisterMediator<RecvMediator>(root.transform.Find("Recv1Window").gameObject);
                break;
            case BroadCommand.BroadCommand_AddValue:
                SendNoticeToMediator(notice);
                break;
        }
        base.Execute(notice);
    }
}

