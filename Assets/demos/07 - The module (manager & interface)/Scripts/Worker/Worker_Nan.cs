using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

/// <summary>
/// 描述：
/// 作者： 
/// </summary>
public class Worker_Nan {

    public const int Index = 70000;

    public static void RegisterCommands()
    {
        Facade.RegisterCommand(MapCommand.Index, new MapCommand());
        Facade.RegisterCommand(RadarCommand.Index, new RadarCommand());

        Facade.SendNotice(MapCommand.Index, MapCommand.Command_Show);
        Facade.SendNotice(RadarCommand.Index, RadarCommand.Command_Show);

    }
}
