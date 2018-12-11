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
    #region properties...

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

    #endregion

    protected override void OnInitialize()
    {
		this.view = this.AttachView<CodeGenView>();
        that = this;
        // TODO: put your init logic here


        base.OnInitialize();
    }

    #region state keeping...

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

    #endregion

    public override void UpdateViewContent()
    {
        // TODO: update the view content here


        this.view.txtName.text = this.viewObj.name;

        base.UpdateViewContent();
    }

    protected override void SetEventListener()
    {
        // TODO: set the event listener function here


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
        // TODO: handle all notices from command
        switch (notice.GetType())
        {
            case Command.Command_Show:
                CodeGenVo vo = notice.GetBody<CodeGenVo>();
                SetViewObj(vo);
                break;
			case Command.Command_Reactive:
                LoadViewObj();
                break;
            // TODO: others type notice...



        }
        base.HandleNotice(notice);
    }

    protected override void OnDispose()
    {
		SaveViewObj();
        that = null;
        // TODO: dispose other resources


        base.OnDispose();
    }
}

/// <summary>
/// your logic view object
/// </summary>
public class CodeGenVo
{
    public string name { get; set; }

}

