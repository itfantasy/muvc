using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

public class SubPageMediator : Mediator {

    TableMediator parent
    {
        get
        {
            return this._parent as TableMediator;
        }
    }

    protected override void OnInitialize()
    {
        base.OnInitialize();
    }
}
