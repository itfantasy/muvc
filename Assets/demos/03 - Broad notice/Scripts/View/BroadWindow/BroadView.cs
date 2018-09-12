using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using itfantasy.umvc;

public class BroadView : View
{
    public Button button;

    protected override void OnInitialize()
    {
        this.button = this.transform.Find("Image/Button").GetComponent<Button>();
        base.OnInitialize();
    }
}

