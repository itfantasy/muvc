using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

/// <summary>
/// 描述：
/// 作者： 
/// </summary>
public class TweenMediator : Mediator {

    public Button button;
    public Image image;

    protected override void OnInitialize()
    {
        this.image = this.transform.Find("Image").GetComponent<Image>();
        this.button = this.transform.Find("Image/Button").GetComponent<Button>();
        base.OnInitialize();
    }

    protected override void SetEventListener()
    {
        EventTriggerListener.Get(this.button.gameObject).onClick = this.OnClick;
        base.SetEventListener();
    }

    protected override void OnClick(GameObject go)
    {
        SendNotice(TweenCommand.Command_Close);
        base.OnClick(go);
    }

    protected override void OnShowing()
    {
        iTween.ScaleFrom(this.image.gameObject, Vector3.one * 0.6f, 0.1f);
        iTween.ScaleTo(this.image.gameObject, Vector3.one, 0.7f);
        base.OnShowing();
    }

    public override void OnClosing(Action callback)
    {
        iTween.ScaleTo(this.image.gameObject, Vector3.one * 0.6f, 0.7f);
        _callback = callback;
        Invoke("DelayClose", 0.4f);
    }

    Action _callback;

    void DelayClose()
    {
        base.OnClosing(_callback);
    }

}
