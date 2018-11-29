using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

public class Worker_K {

    public const int Index = 100000;

    public static void RegisterCommands()
    {
        Facade.RegisterCommand(CodeGenCommand.Index, new CodeGenCommand());
        Facade.SendNotice(CodeGenCommand.Index, Command.Command_Show, new object[] { new CodeGenVo() { name = "K" } });
    }
}
