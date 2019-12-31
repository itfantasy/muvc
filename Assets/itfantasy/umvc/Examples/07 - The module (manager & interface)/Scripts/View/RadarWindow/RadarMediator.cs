using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

/// <summary>
/// 描述：
/// 作者： 
/// </summary>
public class RadarMediator : Mediator {

    Text posText;

    protected override void OnInitialize()
    {
        this.posText = this.transform.Find("Image/PosText").GetComponent<Text>();
        base.OnInitialize();
    }

    protected override void OnShowing()
    {
        posText.text = MapModule.ins.GetHeroPosition().ToString();
        base.OnShowing();
    }
}
