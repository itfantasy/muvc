using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using itfantasy.umvc;

// using TableVo = YourLogicVo;

public class TableMediator : Mediator
{
    TableCommand command
    {
        get
        {
            return this._command as TableCommand;
        }
    }

    public TableVo viewObj
    {
        get
        {
            return this._viewObj as TableVo;
        }
    }

    static TableMediator that;

    List<Button> buttonList = new List<Button>();
    List<Mediator> mediatorList = new List<Mediator>();

    protected override void OnInitialize()
    {
        that = this;

        buttonList.Add(this.transform.Find("Image/Button1").GetComponent<Button>());
        buttonList.Add(this.transform.Find("Image/Button2").GetComponent<Button>());
        buttonList.Add(this.transform.Find("Image/Button3").GetComponent<Button>());

        mediatorList.Add(AttachSubMediator<SubPageMediator>(this.transform.Find("Image1").gameObject));
        mediatorList.Add(AttachSubMediator<SubPageMediator>(this.transform.Find("Image2").gameObject));
        mediatorList.Add(AttachSubMediator<SubPageMediator>(this.transform.Find("Image3").gameObject));

        base.OnInitialize();
    }

    public override void UpdateViewContent()
    {    
        

        base.UpdateViewContent();
    }

    public void ShowSubPage(int index)
    {
        for (int i = 1; i <= mediatorList.Count; i++)
        {
            if(index == i)
            {
                mediatorList[i - 1].Show();
            }
            else
            {
                mediatorList[i - 1].Close();
            }
        }
    }

    protected override void SetEventListener()
    {
        foreach(Button button in buttonList)
        {
            EventTriggerListener.Get(button.gameObject).onClick = this.OnClick;
        }
        base.SetEventListener();
    }

    protected override void OnClick(GameObject go)
    {
        string name = go.name;
        int index = int.Parse(name.Replace("Button", ""));
        ShowSubPage(index);
        base.OnClick(go);
    }

    public override void HandleNotice(INotice notice)
    {
        switch (notice.GetType())
        {
            case Command.Command_Show:
                TableVo vo = notice.GetBody<TableVo>();
                SetViewObj(vo);
                ShowSubPage(viewObj.index);
                break;
			case Command.Command_Reactive:
                LoadViewObj();
                break;
        }
        base.HandleNotice(notice);
    }

    protected override void OnDispose()
    {
		SaveViewObj();
        that = null;
        base.OnDispose();
    }
}

public class TableVo
{
    public int index { get; set; }

    public TableVo(int index)
    {
        this.index = index;
    }
}

