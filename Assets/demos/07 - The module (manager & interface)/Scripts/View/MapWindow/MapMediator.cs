using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

/// <summary>
/// 描述：
/// 作者： 
/// </summary>
public class MapMediator : Mediator {

    MapModule module;

    protected override void OnInitialize()
    {
        this.module = AttachComponent<MapModule>();
        base.OnInitialize();
    }

}
