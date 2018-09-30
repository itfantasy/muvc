using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

/// <summary>
/// 描述：
/// 作者： 
/// </summary>
public class PreACommand : Command {

    public const int Index = Worker_Yue.Index + 100;

    public override void Execute(INotice notice)
    {
        switch(notice.GetType())
        {
            case Command_Show:
                GameObject root = GameObject.Find("UIRoot");
                RegisterMediator<PreAMediator>(root.transform.Find("PreAWindow").gameObject, true);
                break;
            case Command_Close:
                RemoveMediator();
                break;
        }
        base.Execute(notice);
    }
}
