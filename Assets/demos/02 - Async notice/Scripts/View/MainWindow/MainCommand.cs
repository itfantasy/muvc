using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

/// <summary>
/// 描述：
/// 作者： 
/// </summary>
public class MainCommand : Command {

    public const int MainCommand_Index = Worker_Light.CommandIndex + 100;
    public const int MainCommand_Show = MainCommand_Index + 1;
    public const int MainCommand_Close = MainCommand_Index + 2;
    public const int MainCommand_OK = MainCommand_Index + 3;

    public override void Execute(Notice notice)
    {
        switch(notice.code)
        {
            case MainCommand_Show:
                GameObject root = GameObject.Find("UIRoot");
                RegisterMediator<MainMediator>(root.transform.Find("MainWindow").gameObject);
                break;
            case MainCommand_Close:
                RemoveMediator();
                break;
            case MainCommand_OK:
                SendNotice(SubCommand.SubCommand_Index, notice);
                break;
        }
        base.Execute(notice);
    }
}
