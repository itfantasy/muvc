using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

public class Worker_CZ {

    public const int Index = 80000;

    public static void RegisterCommands()
    {
        Facade.RegisterCommand(TableCommand.Index, new TableCommand());
        Facade.SendNotice(TableCommand.Index, Command.Command_Show, new object[] { new TableVo(1) });
    }
	
}
