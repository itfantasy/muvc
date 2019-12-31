using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using itfantasy.umvc;

// using OtherVo = YourLogicVo;

public class OtherMediator : Mediator
{
    OtherCommand command
    {
        get
        {
            return this._command as OtherCommand;
        }
    }

    OtherVo _viewObj;

    public OtherVo viewObj
    {
        get
        {
            return this._viewObj;
        }
    }

    static OtherMediator that;

    Button button;

    protected override void OnInitialize()
    {
        that = this;
        this.button = this.transform.Find("Image/Button").GetComponent<Button>();
        base.OnInitialize();
    }

    public void SetViewObj(OtherVo vo)
    {
		if (vo != null)
		{
			this._viewObj = vo;
			this.UpdateViewContent();
		}
    }

    private void SaveViewObj()
    {
        if (this._viewObj != null)
        {
            this._command.token = this._viewObj;
        }
    }

    private void LoadViewObj()
    {
		if (this._viewObj == null)
		{
			this.SetViewObj(this._command.token as OtherVo);
		}
    }

    public override void UpdateViewContent()
    {


        base.UpdateViewContent();
    }

    protected override void SetEventListener()
    {
        EventTriggerListener.Get(this.button.gameObject).onClick = this.OnClick;
        base.SetEventListener();
    }

    protected override void OnClick(GameObject go)
    {
        this.command.GotoKeepScene();
        base.OnClick(go);
    }

    public override void HandleNotice(INotice notice)
    {
        switch (notice.GetType())
        {
            case Command.Command_Show:
                OtherVo vo = notice.GetBody<OtherVo>();
                SetViewObj(vo);
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

public class OtherVo
{

}

