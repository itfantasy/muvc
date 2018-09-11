using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace itfantasy.umvc
{
    public class AsyncNotice : Notice
    {
        private Action<object> _callback;
        private object token;

        public AsyncNotice(int code, Action<object> callback, object token)
            : base(code)
        {
            this._callback = callback;
            this.token = token;
        }

        public void Done()
        {
            _callback.Invoke(token);
        }
    }
}

