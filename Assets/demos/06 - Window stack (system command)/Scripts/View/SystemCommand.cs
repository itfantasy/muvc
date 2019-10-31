using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using itfantasy.umvc;

class SystemCommand : Command
{
    public override void Execute(INotice notice)
    {
        switch(notice.GetType())
        {
            case Monitor_Closed:
                WindowStackManager.ins.OnWindowClosed(notice.GetBody()[0].ToString());
                break;
        }

        base.Execute(notice);
    }
}

