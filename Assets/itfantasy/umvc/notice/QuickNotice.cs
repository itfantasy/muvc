using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace itfantasy.umvc
{
    public class QuickNotice : Notice
    {
        private object[] _params;

        public QuickNotice(int code, params object[] _params)
            : base(code)
        {
            this._params = _params;
        }

        public object[] Params
        {
            get
            {
                return _params;
            }
        }
    }
}

