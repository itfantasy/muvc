using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

/// <summary>
/// 描述：
/// 作者： 
/// </summary>
public class WindowStack {

    private static WindowStack _ins;

    public static WindowStack ins
    {
        get
        {
            if(_ins == null)
            {
                _ins = new WindowStack();
            }
            return _ins;
        }
    }
	
	public void PushStack(string windowName, int index, int noticeType, params object[] noticeBody)
    {
        StackNotice stackNotice = new StackNotice(windowName, index, noticeType, noticeBody);
        noticeList.Add(stackNotice);
    }

    public void PopAndSend()
    {
        if(noticeList.Count > 0)
        {
            sendingNotice = noticeList[0];
            Facade.SendNotice(sendingNotice.index, sendingNotice.type, sendingNotice.body);
            noticeList.RemoveAt(0);
        }
    }

    public void ClearStack()
    {
        sendingNotice = null;
        noticeList.Clear();
    }

    public void OnWindowClosed(string windowName)
    {
        if (sendingNotice != null && sendingNotice.windowName == windowName)
        {
            PopAndSend();
        }
    }

    StackNotice sendingNotice = null;
    List<StackNotice> noticeList = new List<StackNotice>();
}

public class StackNotice
{
    public string windowName;
    public int index;
    public int type;
    public object[] body;

    public StackNotice(string windowName, int index, int type, object[] body)
    {
        this.windowName = windowName;
        this.index = index;
        this.type = type;
        this.body = body;
    }
}
