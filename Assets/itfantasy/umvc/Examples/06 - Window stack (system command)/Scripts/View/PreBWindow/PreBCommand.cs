using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

/// <summary>
/// 描述：
/// 作者： 
/// </summary>
public class PreBCommand : Command {

    public const int Index = Worker_Yue.Index + 200;

    public override void Execute(INotice notice)
    {
        switch (notice.GetType())
        {
            case Command_Show:
                GameObject root = GameObject.Find("UIRoot");
                RegisterMediator<PreBMediator>(root.transform.Find("PreBWindow").gameObject, true);
                break;
            case Command_Close:
                RemoveMediator();
                break;
        }
        base.Execute(notice);
    }
}
