using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

/// <summary>
/// 描述：
/// 作者： 
/// </summary>
public class Worker_Ken {

    public const int CommandIndex = 10000;

	public static void RegisterCommands()
    {
        Facade.RegisterCommand(Command1.Index, new Command1());
        Facade.RegisterCommand(Command2.Index, new Command2());

        Facade.SendNotice(Command1.Index, Command1.Command1_Show);
    }
}
