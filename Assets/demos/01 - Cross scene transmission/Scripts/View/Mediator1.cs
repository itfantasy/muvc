using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

/// <summary>
/// 描述：
/// 作者： 
/// </summary>
public class Mediator1 : Mediator {

    View1 view;

    protected override void OnInitialize()
    {
        this.view = this.AttachView<View1>();
        base.OnInitialize();
    }

    protected override void SetEventListener()
    {
        EventTriggerListener.Get(this.view.button.gameObject).onClick = this.OnClick;
        base.SetEventListener();
    }

    protected override void OnClick(GameObject go)
    {
        this.SendNoticeToCommand(new Scene1To2Vo(Command1.Command1_OK, this.view.input.text));
        base.OnClick(go);
    }

    public override void HandleNotice(Notice notice)
    {
        switch(notice.code)
        {
            case Command2.Command2_OK:
                this.view.input.text = (notice as Scene2To1Vo).value;
                break;
        }
        base.HandleNotice(notice);
    }
}
