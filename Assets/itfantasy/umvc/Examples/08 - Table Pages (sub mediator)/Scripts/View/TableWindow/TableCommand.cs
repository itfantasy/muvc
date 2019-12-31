using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using itfantasy.umvc;

public class TableCommand : Command
{
    public const int Index = Worker_CZ.Index + 100;

    TableMediator mediator;

    public override void Execute(INotice notice)
    {
        switch(notice.GetType())
        {
            case Command_Show:
                GameObject go = null;
                GameObject root = GameObject.Find("UIRoot");
                go = root.transform.Find("TabWindow").gameObject;
                this.mediator = RegisterMediator<TableMediator>(go);
				this.SendToMediator(notice);
                break;
            case Command_Close:
                RemoveMediator();
                break;

            case Command_Reactive:
                GameObject rego = null; // must use a sync func to load the rego
                root = GameObject.Find("UIRoot");
                rego = root.transform.Find("TabWindow").gameObject;
                this.mediator = RegisterMediator<TableMediator>(rego);
				this.SendToMediator(notice);
                break;



        }
        base.Execute(notice);
    }
}

