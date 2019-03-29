using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using itfantasy.umvc;

public class CodeGenCommand : Command
{
    public const int Index = Worker_K.Index + 100;

    public override void Execute(INotice notice)
    {
        switch(notice.GetType())
        {
            case Command_Show:
				LoadResource("CodeGenWindow", (go) => { 
					RegisterMediator<CodeGenMediator>(go);
					this.SendToMediator(notice);
                });
                break;
            case Command_Close:
                RemoveMediator();
                break;
            case Command_Reactive:
                GameObject rego = SyncLoadResource("CodeGenWindow"); // must use a sync func to load the rego
                RegisterMediator<CodeGenMediator>(rego);
				this.SendToMediator(notice);
                break;
			case Command_OK:
				// TODO: the Mediator.OK() Logic
				
				break;
			case Command_Cancel:
				SelfNotice(Command_Close);
				break;
			// TODO: others custom notices...


        }
        base.Execute(notice);
    }
}

