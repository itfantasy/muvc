using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

/// <summary>
/// 描述：
/// 作者： 
/// </summary>
public class SubCommand : Command {

    public const int SubCommand_Index = Worker_Light.CommandIndex + 200;
    public const int SubCommand_Show = SubCommand_Index + 1;
    public const int SubCommand_Close = SubCommand_Index + 2;
    public const int SubCommand_OK = SubCommand_Index + 3;

    public override void Execute(Notice notice)
    {
        switch(notice.code)
        {
            case SubCommand_Show:
                GameObject root = GameObject.Find("UIRoot");
                RegisterMediator<SubMediator>(root.transform.Find("SubWindow").gameObject);
                break;
            case SubCommand_Close:
                RemoveMediator();
                break;
            case MainCommand.MainCommand_OK:
                SendNotice(SubCommand_Index, new Notice(SubCommand_Show));
                SendNoticeToMediator(notice);
                break;
        }
        base.Execute(notice);
    }
}
