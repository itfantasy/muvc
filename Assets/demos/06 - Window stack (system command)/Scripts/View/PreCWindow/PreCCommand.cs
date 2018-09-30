using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

/// <summary>
/// 描述：
/// 作者： 
/// </summary>
public class PreCCommand : Command {

    public const int Index = Worker_Yue.Index + 300;

    public override void Execute(INotice notice)
    {
        switch (notice.GetType())
        {
            case Command_Show:
                GameObject root = GameObject.Find("UIRoot");
                RegisterMediator<PreCMediator>(root.transform.Find("PreCWindow").gameObject, true);
                break;
            case Command_Close:
                RemoveMediator();
                break;
        }
        base.Execute(notice);
    }
}
