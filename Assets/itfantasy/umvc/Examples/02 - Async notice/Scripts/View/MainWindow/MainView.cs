using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using itfantasy.umvc;

public class MainView : View
{
    public Button button;
    public InputField input;
    public Text text;

    protected override void OnInitialize()
    {
        this.button = this.transform.Find("Image/Button").GetComponent<Button>();
        this.input = this.transform.Find("Image/InputField").GetComponent<InputField>();
        this.text = this.transform.Find("Image/Text").GetComponent<Text>();
        base.OnInitialize();
    }
}

