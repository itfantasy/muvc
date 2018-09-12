using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using itfantasy.umvc;

public class Worker_Light
{

    public const int CommandIndex = 20000;

    public static void RegisterCommands()
    {
        Facade.RegisterCommand(MainCommand.MainCommand_Index, new MainCommand());
        Facade.RegisterCommand(SubCommand.SubCommand_Index, new SubCommand());

        Facade.SendNotice(MainCommand.MainCommand_Index, new Notice(MainCommand.MainCommand_Show));
    }
}

