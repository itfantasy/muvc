using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

/// <summary>
/// 描述：
/// 作者： 
/// </summary>
public class Mediator2 : Mediator {

    View2 view;

    protected override void OnInitialize()
    {
        this.view = this.AttachView<View2>();
        base.OnInitialize();
    }

    protected override void SetEventListener()
    {
        EventTriggerListener.Get(this.view.button.gameObject).onClick = this.OnClick;
        base.SetEventListener();
    }

    protected override void OnClick(GameObject go)
    {
        this.SendNoticeToCommand(new Scene2To1Vo(Command2.Command2_OK, "场景2传值给场景1"));
        base.OnClick(go);
    }

    public override void HandleNotice(Notice notice)
    {
        switch (notice.code)
        {
            case Command1.Command1_OK:
                this.view.input.text = (notice as Scene1To2Vo).value;
                break;
        }
        base.HandleNotice(notice);
    }
}
