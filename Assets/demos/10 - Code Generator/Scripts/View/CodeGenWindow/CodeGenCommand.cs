using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using itfantasy.umvc;

public class CodeGenCommand : Command
{
    public const int Index = Worker_K.Index + 100;

    CodeGenMediator mediator;

    public override void Execute(INotice notice)
    {
        switch(notice.GetType())
        {
            case Command_Show:
                GameObject go = null;
                GameObject root = GameObject.Find("UIRoot");
                go = root.transform.Find("CodeGenWindow").gameObject;
                this.mediator = RegisterMediator<CodeGenMediator>(go);
				this.SendToMediator(notice);
                break;
            case Command_Close:
                RemoveMediator();
                break;

            case Command_Reactive:
                GameObject rego = null; // must use a sync func to load the rego
                root = GameObject.Find("UIRoot");
                rego = root.transform.Find("CodeGenWindow").gameObject;
                this.mediator = RegisterMediator<CodeGenMediator>(rego);
				this.SendToMediator(notice);
                break;



        }
        base.Execute(notice);
    }
}

