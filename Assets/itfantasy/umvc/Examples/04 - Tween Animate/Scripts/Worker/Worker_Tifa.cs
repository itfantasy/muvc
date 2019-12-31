using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

/// <summary>
/// 描述：
/// 作者： 
/// </summary>
public class Worker_Tifa {

    public const int Index = 40000;

    public static void RegisterCommands()
    {
        Facade.RegisterCommand(ComCommand.Index, new ComCommand());
        Facade.RegisterCommand(TweenCommand.Index, new TweenCommand());

        Facade.SendNotice(ComCommand.Index, ComCommand.Command_Show);
        
    }
}
