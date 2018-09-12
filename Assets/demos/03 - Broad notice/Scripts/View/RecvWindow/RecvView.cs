using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using itfantasy.umvc;

class RecvView : View
{
    public Text text;

    private int _num;
    public int num
    {
        get
        {
            return _num;
        }
        set
        {
            _num = value;
            this.text.text = _num.ToString();
        }
    }

    protected override void OnInitialize()
    {
        this.text = this.transform.Find("Image/Text").GetComponent<Text>();
        base.OnInitialize();
    }
}

