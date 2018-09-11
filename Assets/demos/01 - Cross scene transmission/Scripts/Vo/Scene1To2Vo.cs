using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using itfantasy.umvc;

class Scene1To2Vo : Notice
{
    public string value;

    public Scene1To2Vo(int code, string value)
        : base(code)
    {
        this.value = value;
    }
}

