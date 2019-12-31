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
        Action<INotice> _callback;
        object _token;

        public object token
        {
            get
            {
                return this._token;
            }
        }

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
            if (this._body != null)
            {
                foreach (object val in this._body)
                {
                    if (val is T)
                    {
                        return (T)val;
                    }
                }
            }
            return default(T);
        }

        public void Finish()
        {
            if (this._callback != null)
            {
                this._callback.Invoke(this);
                this._callback = null;
            }
        }

        public void SetCallback(Action<INotice> callback, object token)
        {
            this._callback = callback;
            this._token = token;
        }

    }
}
