using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

/// <summary>
/// 描述：
/// 作者： 
/// </summary>
public class SubCommand : Command {

    public const int Index = Worker_Light.Index + 200;
    public const int SubCommand_Show = Index + 1;
    public const int SubCommand_Close = Index + 2;
    public const int SubCommand_OK = Index + 3;

    public override void Execute(INotice notice)
    {
        switch(notice.GetType())
        {
            case SubCommand_Show:
                GameObject root = GameObject.Find("UIRoot");
                RegisterMediator<SubMediator>(root.transform.Find("SubWindow").gameObject);
                break;
            case SubCommand_Close:
                RemoveMediator();
                break;
            case SubCommand_OK:
                SendNotice(Index, SubCommand_Show);
                SendToMediator(notice);
                break;
        }
        base.Execute(notice);
    }

    public void FinishNotice(INotice notice)
    {
        this.SendNotice(Index, SubCommand_Close);
        if (notice != null)
        {
            notice.Finish();
        }
    }
}
