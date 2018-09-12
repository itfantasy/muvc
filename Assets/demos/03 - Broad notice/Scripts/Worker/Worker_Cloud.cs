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

    public static void RegisterCommands()
    {
        Facade.RegisterCommand(BroadCommand.BroadCommand_Index, new BroadCommand());
        Facade.RegisterCommand(Recv1Command.Recv1Command_Index, new Recv1Command());
        Facade.RegisterCommand(Recv2Command.Recv2Command_Index, new Recv2Command());

        Facade.SendNotice(BroadCommand.BroadCommand_Index, new Notice(BroadCommand.BroadCommand_Show));
        Facade.SendNotice(Recv1Command.Recv1Command_Index, new Notice(Recv1Command.Recv1Command_Show));
        Facade.SendNotice(Recv2Command.Recv2Command_Index, new Notice(Recv2Command.Recv2Command_Show));
    }
}
