using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

/// <summary>
/// 描述：
/// 作者： 
/// </summary>
public class Worker_Yue {

    public const int Index = 60000;

    public static void RegisterCommands()
    {
        Facade.RegisterCommand(Facade.SystemIndex, new SystemCommand());
        Facade.RegisterCommand(PreACommand.Index, new PreACommand());
        Facade.RegisterCommand(PreBCommand.Index, new PreBCommand());
        Facade.RegisterCommand(PreCCommand.Index, new PreCCommand());

        WindowStack.ins.PushStack("PreAWindow", PreACommand.Index, Command.Command_Show);
        WindowStack.ins.PushStack("PreBWindow", PreBCommand.Index, Command.Command_Show);
        WindowStack.ins.PushStack("PreCWindow", PreCCommand.Index, Command.Command_Show);
    }
	
}
