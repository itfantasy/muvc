using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

/// <summary>
/// 描述：
/// 作者： 
/// </summary>
public class MapMediator : Mediator {

    protected override void OnInitialize()
    {
        AttachComponent<MapModule>();
        base.OnInitialize();
    }

}
