using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

/// <summary>
/// 描述：
/// 作者： 
/// </summary>
public class BroadCommand : Command {

    public const int BroadCommand_Index = Worker_Cloud.CommandIndex + 100;
    public const int BroadCommand_Show = BroadCommand_Index + 1;
    public const int BroadCommand_AddValue = BroadCommand_Index + 2;

    public override void Execute(Notice notice)
    {
        switch(notice.code)
        {
            case BroadCommand_Show:
                GameObject root = GameObject.Find("UIRoot");
                RegisterMediator<BroadMediator>(root.transform.Find("BroadWindow").gameObject);
                break;
            case BroadCommand_AddValue:
                BroadNotice(notice);
                break;
        }
        base.Execute(notice);
    }
}
