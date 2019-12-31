using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

/// <summary>
/// 描述：
/// 作者： 
/// </summary>
public class BroadCommand : Command {

    public const int Index = Worker_Cloud.Index + 100;
    public const int BroadCommand_Show = Index + 1;

    public override void Execute(INotice notice)
    {
        switch(notice.GetType())
        {
            case BroadCommand_Show:
                GameObject root = GameObject.Find("UIRoot");
                RegisterMediator<BroadMediator>(root.transform.Find("BroadWindow").gameObject);
                break;
        }
        base.Execute(notice);
    }

    public void BroadAddValue(int value)
    {
        BroadNotice(Worker_Cloud.Broad_AddValue, 10);
    }
}
