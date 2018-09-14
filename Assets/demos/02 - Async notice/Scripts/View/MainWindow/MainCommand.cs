using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

/// <summary>
/// 描述：
/// 作者： 
/// </summary>
public class MainCommand : Command {

    public const int Index = Worker_Light.Index + 100;
    public const int MainCommand_Show = Index + 1;
    public const int MainCommand_Close = Index + 2;
    public const int MainCommand_OK = Index + 3;

    public override void Execute(INotice notice)
    {
        switch(notice.GetType())
        {
            case MainCommand_Show:
                GameObject root = GameObject.Find("UIRoot");
                RegisterMediator<MainMediator>(root.transform.Find("MainWindow").gameObject);
                break;
            case MainCommand_Close:
                RemoveMediator();
                break;
        }
        base.Execute(notice);
    }

    public void AsyncMainToSub(Action<object> callback, object token)
    {
        SendAsyncNotice(SubCommand.Index, SubCommand.SubCommand_OK, callback, token);
    }
}
