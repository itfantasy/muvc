using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

/// <summary>
/// 描述：
/// 作者： 
/// </summary>
public class Worker_Noctis {

    public const int Index = 50000;

    public static void RegisterCommands()
    {
        Facade.RegisterCommand(AMainCommand.Index, new AMainCommand());
        Facade.RegisterCommand(ASubCommand.Index, new ASubCommand());

        Facade.SendNotice(AMainCommand.Index, AMainCommand.Command_Show);
    }
}
