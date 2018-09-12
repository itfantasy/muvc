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

        public object[] paramArray
        {
            get
            {
                return _params;
            }
        }
    }
}

