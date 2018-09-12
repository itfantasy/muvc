using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using itfantasy.umvc;

class MainToSubVo : AsyncNotice
{

    public MainToSubVo(int code, Action<object> callback, object token)
        : base(code, callback, token)
    {
        
    }

}

