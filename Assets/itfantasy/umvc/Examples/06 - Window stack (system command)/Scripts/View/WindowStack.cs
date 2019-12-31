using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using itfantasy.umvc;

public class WindowStackManager
{
    private static WindowStackManager _ins;

    public static WindowStackManager ins
    {
        get
        {
            if (_ins == null)
            {
                _ins = new WindowStackManager();
            }
            return _ins;
        }
    }

    private List<WindowStack> _stacks = new List<WindowStack>();
    private List<WindowStack> _dirtyStacks = new List<WindowStack>();

    public WindowStack CreateWindowStack()
    {
        WindowStack stack = new WindowStack();
        this._stacks.Add(stack);
        return stack;
    }

    public void OnWindowClosed(string windowName)
    {
        foreach(WindowStack stack in _dirtyStacks)
        {
            _stacks.Remove(stack);
        }
        _dirtyStacks.Clear();
        foreach (WindowStack stack in _stacks)
        {
            if (stack.curWindowName == windowName)
            {
                if (stack.PopAndShowNext())
                {
                    stack.ClearStack();
                    _dirtyStacks.Add(stack);
                }
            }
        }
    }
}

public class WindowStack {

    bool showing = false;

    public WindowStack PushWindow(string windowName, int index, params object[] noticeBody)
    {
        StackShowNotice stackNotice = new StackShowNotice(windowName, index, noticeBody);
        noticeList.Add(stackNotice);
        return this;
    }

    public void BeginShow()
    {
        if (!this.showing)
        {
            this.showing = true;
            this.PopAndShowNext();
        }
    }

    public bool PopAndShowNext()
    {
        if (showing && noticeList.Count > 0)
        {
            sendingNotice = noticeList[0];
            Facade.SendNotice(sendingNotice.index, Command.Command_Show, sendingNotice.body);
            noticeList.RemoveAt(0);
        }
        return showing && noticeList.Count <= 0;
    }

    public void ClearStack()
    {
        sendingNotice = null;
        noticeList.Clear();
    }

    public string curWindowName
    {
        get
        {
            if (sendingNotice != null)
            {
                return sendingNotice.windowName;
            }
            return "";
        }
    }
    StackShowNotice sendingNotice = null;
    List<StackShowNotice> noticeList = new List<StackShowNotice>();
}

public class StackShowNotice
{
    public string windowName;
    public int index;
    public object[] body;

    public StackShowNotice(string windowName, int index, object[] body)
    {
        this.windowName = windowName;
        this.index = index;
        this.body = body;
    }
}
