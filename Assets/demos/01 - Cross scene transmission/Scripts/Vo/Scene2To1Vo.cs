using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using itfantasy.umvc;

class Scene2To1Vo : Notice
{
    public string value;

    public Scene2To1Vo(int code, string value)
        : base(code)
    {
        this.value = value;
    }
}

