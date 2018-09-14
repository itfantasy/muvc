using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using itfantasy.umvc;

public class Worker_Light
{

    public const int Index = 20000;

    public static void RegisterCommands()
    {
        Facade.RegisterCommand(MainCommand.Index, new MainCommand());
        Facade.RegisterCommand(SubCommand.Index, new SubCommand());

        Facade.SendNotice(MainCommand.Index, MainCommand.MainCommand_Show);
    }
}

