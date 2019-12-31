using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using itfantasy.umvc;

// using KeepVo = YourLogicVo;

public class KeepMediator : Mediator
{
    KeepCommand command
    {
        get
        {
            return this._command as KeepCommand;
        }
    }

    KeepVo _viewObj;

    public KeepVo viewObj
    {
        get
        {
            return this._viewObj;
        }
    }

    static KeepMediator that;

    Button button;
    InputField input;
    Slider slider;

    protected override void OnInitialize()
    {
        that = this;
        this.button = this.transform.Find("Image/Button").GetComponent<Button>();
        this.input = this.transform.Find("Image/InputField").GetComponent<InputField>();
        this.slider = this.transform.Find("Image/Slider").GetComponent<Slider>();
        base.OnInitialize();
    }

    public void SetViewObj(KeepVo vo)
    {
		if (vo != null)
		{
			this._viewObj = vo;
			this.UpdateViewContent();
		}
    }

    private void SaveViewObj()
    {
        this._command.token = this._viewObj;
    }

    private void LoadViewObj()
    {
		if (this._viewObj == null)
		{
			this.SetViewObj(this._command.token as KeepVo);
		}
    }

    public override void UpdateViewContent()
    {
        this.input.text = this.viewObj.inputText;
        this.slider.value = this.viewObj.sliderValue;
        
        base.UpdateViewContent();
    }

    protected override void SetEventListener()
    {
        EventTriggerListener.Get(this.button.gameObject).onClick = this.OnClick;
        base.SetEventListener();
    }

    protected override void OnClick(GameObject go)
    {
        this.viewObj.inputText = this.input.text;
        this.viewObj.sliderValue = this.slider.value;

        SendNotice(Command.Command_OK);
        base.OnClick(go);
    }

    public override void HandleNotice(INotice notice)
    {
        switch (notice.GetType())
        {
            case Command.Command_Show:
                KeepVo vo = notice.GetBody<KeepVo>();
                if(vo == null)
                {
                    vo = new KeepVo();
                }
                SetViewObj(vo);
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

public class KeepVo
{
    public string inputText = "";
    public float sliderValue = 0.0f;
}

