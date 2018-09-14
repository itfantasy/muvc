using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

/// <summary>
/// 描述：
/// 作者： 
/// </summary>
public class Worker_Cloud {

    public const int CommandIndex = 30000;
    public const int Broad_AddValue = CommandIndex + 1;

    public static void RegisterCommands()
    {
        Facade.RegisterCommand(BroadCommand.Index, new BroadCommand());
        Facade.RegisterCommand(Recv1Command.Index, new Recv1Command());
        Facade.RegisterCommand(Recv2Command.Index, new Recv2Command());

        Facade.SendNotice(BroadCommand.Index, BroadCommand.BroadCommand_Show);
        Facade.SendNotice(Recv1Command.Index, Recv1Command.Recv1Command_Show);
        Facade.SendNotice(Recv2Command.Index, Recv2Command.Recv2Command_Show);
    }
}
