using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using itfantasy.umvc;

public class CodeGenView : View
{

    public Image imgBg;
    public Text txtName;
    public Image imgHead;
    public Button btnOK;

    protected override void OnInitialize()
    {

        this.imgBg = this.transform.Find("imgBg").GetComponent<Image>();
        this.txtName = this.transform.Find("imgBg/txtName").GetComponent<Text>();
        this.imgHead = this.transform.Find("imgBg/imgHead").GetComponent<Image>();
        this.btnOK = this.transform.Find("imgBg/btnOK").GetComponent<Button>();

        base.OnInitialize();
    }
}

