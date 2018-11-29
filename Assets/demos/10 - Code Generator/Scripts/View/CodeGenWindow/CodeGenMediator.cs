using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using itfantasy.umvc;

// using CodeGenVo = YourLogicVo;

public class CodeGenMediator : Mediator
{
    CodeGenCommand command
    {
        get
        {
            return this._command as CodeGenCommand;
        }
    }

	CodeGenView view;

    CodeGenVo _viewObj;

    public CodeGenVo viewObj
    {
        get
        {
            return this._viewObj;
        }
    }

    static CodeGenMediator that;

    protected override void OnInitialize()
    {
		this.view = this.AttachView<CodeGenView>();
        that = this;
        base.OnInitialize();
    }

    public void SetViewObj(CodeGenVo vo)
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
			this.SetViewObj(this._command.token as CodeGenVo);
		}
    }

    public override void UpdateViewContent()
    {

        this.view.txtName.text = this.viewObj.name;

        base.UpdateViewContent();
    }

    protected override void SetEventListener()
    {
        EventTriggerListener.Get(this.view.btnOK.gameObject).onClick = this.OnClick;

        base.SetEventListener();
    }

    protected override void OnClick(GameObject go)
    {
        Debug.Log("the button has been clicked!");

        base.OnClick(go);
    }

    public override void HandleNotice(INotice notice)
    {
        switch (notice.GetType())
        {
            case Command.Command_Show:
                CodeGenVo vo = notice.GetBody<CodeGenVo>();
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

public class CodeGenVo
{
    public string name { get; set; }

}

