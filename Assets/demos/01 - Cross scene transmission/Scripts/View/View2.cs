using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

/// <summary>
/// 描述：
/// 作者： 
/// </summary>
public class View2 : View {

    public Button button;
    public InputField input;

    protected override void OnInitialize()
    {
        this.button = this.transform.Find("Button").GetComponent<Button>();
        this.input = this.transform.Find("InputField").GetComponent<InputField>();
        base.OnInitialize();
    }
}
