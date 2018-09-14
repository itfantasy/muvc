using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace itfantasy.umvc
{
    public class Notice : INotice
    {
        int _type;
        object[] _body;
        Action<object> _callback;
        object _token;

        public object tag { get; set; }

        public Notice(int type, object[] body)
        {
            this._type = type;
            this._body = body;
        }

        public new int GetType()
        {
            return this._type;
        }

        public object[] GetBody()
        {
            return this._body;
        }

        public T GetBody<T>()
        {
            foreach(object val in this._body)
            {
                if(val is T)
                {
                    return (T)val;
                }
            }
            return default(T);
        }

        public void Finish()
        {
            if (this._callback != null)
            {
                this._callback.Invoke(this._token);
            }
        }

        public void SetCallback(Action<object> callback, object token)
        {
            this._callback = callback;
            this._token = token;
        }

        public object GetToken()
        {
            return this._token;
        }
    }
}
