﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

/// <summary>
/// 描述：
/// 作者： 
/// </summary>
public class AMainCommand : Command {

    public const int Index = Worker_Noctis.Index + 100;

    public override void Execute(INotice notice)
    {
        switch(notice.GetType())
        {
            case Command_Show:
                GameObject root = GameObject.Find("UIRoot");
                RegisterMediator<AMainMediator>(root.transform.Find("MainWindow").gameObject);
                break;
            case Command_Close:
                RemoveMediator();
                break;
            case Command_Reactive:
                SendNotice(Index, Command_Show);
                break;
        }
        base.Execute(notice);
    }

    public void AsyncMainToSub(Action<INotice> callback, object token)
    {
        SendAsyncNotice(ASubCommand.Index, ASubCommand.Command_OK, callback, token);
    }
}
