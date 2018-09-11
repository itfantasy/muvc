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
        Facade.RegisterCommand(Command1.Command1_Index, new Command1());
        Facade.RegisterCommand(Command2.Command2_Index, new Command2());

        Facade.SendNotice(Command1.Command1_Index, new Notice(Command1.Command1_Show));
    }
}
