using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

/// <summary>
/// 描述：
/// 作者： 
/// </summary>
public class Worker_J {

    public const int Index = 90000;

    public static void RegisterCommands()
    {
        Facade.RegisterCommand(KeepCommand.Index, new KeepCommand());
        Facade.RegisterCommand(OtherCommand.Index, new OtherCommand());

        Facade.SendNotice(KeepCommand.Index, Command.Command_Show);
    }
}
