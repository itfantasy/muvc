using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace itfantasy.umvc
{
    public class AsyncNotice : Notice
    {
        private Action<object> _callback;
        private object _token;

        public object token
        {
            get
            {
                return this._token;
            }
        }

        public AsyncNotice(int code, Action<object> callback, object token)
            : base(code)
        {
            this._callback = callback;
            this._token = token;
        }

        public void Done()
        {
            _callback.Invoke(_token);
        }
    }
}

