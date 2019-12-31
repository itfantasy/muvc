using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using itfantasy.umvc;

public class KeepCommand : Command
{
    public const int Index = Worker_J.Index + 100;

    KeepMediator mediator;

    public override void Execute(INotice notice)
    {
        switch(notice.GetType())
        {
            case Command_Show:
                GameObject go = null;
                GameObject root = GameObject.Find("UIRoot");
                go = root.transform.Find("KeepWindow").gameObject;
                this.mediator = RegisterMediator<KeepMediator>(go);
				this.SendToMediator(notice);
                break;
            case Command_Close:
                RemoveMediator();
                break;

            case Command_Reactive:
                GameObject rego = null; // must use a sync func to load the rego
                root = GameObject.Find("UIRoot");
                rego = root.transform.Find("KeepWindow").gameObject;
                this.mediator = RegisterMediator<KeepMediator>(rego);
                this.SendToMediator(notice);
                break;

            case Command_OK:
                SendNotice(OtherCommand.Index, Command_OK);
                break;

        }
        base.Execute(notice);
    }
}

