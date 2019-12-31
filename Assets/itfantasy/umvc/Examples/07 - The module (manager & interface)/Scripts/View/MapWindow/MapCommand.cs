using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

/// <summary>
/// 描述：
/// 作者： 
/// </summary>
public class MapCommand : Command {

    public const int Index = Worker_Nan.Index + 100;

    public override void Execute(INotice notice)
    {
        switch (notice.GetType())
        {
            case Command.Command_Show:
                GameObject root = GameObject.Find("MapRoot");
                RegisterMediator<MapMediator>(root.gameObject);
                break;
        }
        base.Execute(notice);
    }
}
