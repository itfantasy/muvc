using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

/// <summary>
/// 描述：
/// 作者： 
/// </summary>
public class ComCommand : Command {

    public const int Index = Worker_Tifa.Index + 100;
    public const int Command_Show = Index + 1;

    public override void Execute(INotice notice)
    {
        switch(notice.GetType())
        {
            case Command_Show:
                GameObject root = GameObject.Find("UIRoot");
                RegisterMediator<ComMediator>(root.transform.Find("ComWindow").gameObject);
                break;
        }
        base.Execute(notice);
    }

    public void ShowTweenWindow()
    {
        SendNotice(TweenCommand.Index, TweenCommand.Command_Show);
    }
}
