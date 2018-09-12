using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using itfantasy.umvc;

public class SubView : View
{
    public Button button;
    public Text text;

    protected override void OnInitialize()
    {
        this.button = this.transform.Find("Image/Button").GetComponent<Button>();
        this.text = this.transform.Find("Image/Text").GetComponent<Text>();
        base.OnInitialize();
    }
}

