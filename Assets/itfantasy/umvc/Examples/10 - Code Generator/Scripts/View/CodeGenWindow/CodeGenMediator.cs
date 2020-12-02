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

    public CodeGenVo viewObj
    {
        get
        {
            return this._viewObj as CodeGenVo;
        }
    }

    static CodeGenMediator that { get; set; }

	#endregion

    protected override void OnInitialize()
    {
		this.view = this.AttachView<CodeGenView>();
        that = this;
		// TODO: put your init logic here

        base.OnInitialize();
    }

    public override void UpdateViewContent()
    {    
        // TODO: update the view content here


        base.UpdateViewContent();
    }

    protected override void SetEventListener()
    {
		// TODO: set the event listener function here
        SetClick(this.view.btnOK, (go) => {
            OK();
        });
        SetClick(this.view.btnCancel, (go) => {
            Cancel();
        });

        base.SetEventListener();
    }

    protected override void OnClick(GameObject go)
    {
        
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
			// TODO: others custom notices...


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

