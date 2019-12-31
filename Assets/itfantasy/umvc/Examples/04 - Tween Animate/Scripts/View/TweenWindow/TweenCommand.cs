using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

/// <summary>
/// 描述：
/// 作者： 
/// </summary>
public class TweenCommand : Command {

    public const int Index = Worker_Tifa.Index + 200;

    public override void Execute(INotice notice)
    {
        switch(notice.GetType())
        {
            case Command_Show:
                GameObject root = GameObject.Find("UIRoot");
                RegisterMediator<TweenMediator>(root.transform.Find("TweenWindow").gameObject);
                break;
            case Command_Close:
                RemoveMediator();
                break;
        }
        base.Execute(notice);
    }
}
