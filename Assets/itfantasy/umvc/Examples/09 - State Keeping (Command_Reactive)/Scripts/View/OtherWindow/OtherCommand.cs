using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using itfantasy.umvc;

public class OtherCommand : Command
{
    public const int Index = Worker_J.Index + 200;

    OtherMediator mediator;

    public override void Execute(INotice notice)
    {
        switch(notice.GetType())
        {
            case Command_Show:
                GameObject go = null;
                GameObject root = GameObject.Find("UIRoot");
                go = root.transform.Find("OtherWindow").gameObject;
                this.mediator = RegisterMediator<OtherMediator>(go);
				this.SendToMediator(notice);
                break;
            case Command_Close:
                RemoveMediator();
                break;
            case Command_OK:
                ChangeScene("OtherScene", (token) => {
                    SelfNotice(Command_Show);
                });
                break;

        }
        base.Execute(notice);
    }

    public void GotoKeepScene()
    {
        SelfNotice(Command_Close);
        ChangeScene("KeepScene");
    }
}

