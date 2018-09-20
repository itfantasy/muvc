using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

/// <summary>
/// 描述：
/// 作者： 
/// </summary>
public class AMainMediator : Mediator {

    MainView view;
    AMainCommand cmd
    {
        get
        {
            return this._command as AMainCommand;
        }
    }

    static AMainMediator that;

    protected override void OnInitialize()
    {
        that = this;
        this.view = AttachView<MainView>();
        base.OnInitialize();
    }

    protected override void SetEventListener()
    {
        EventTriggerListener.Get(this.view.button.gameObject).onClick = this.OnClick;
        base.SetEventListener();
    }

    protected override void OnClick(GameObject go)
    {
        this.cmd.AsyncMainToSub((token) =>
        {
            that.view.text.text = token.ToString();
        }, this.view.input.text);
        base.OnClick(go);
    }

    public override void HandleNotice(INotice notice)
    {
        base.HandleNotice(notice);
    }
}
