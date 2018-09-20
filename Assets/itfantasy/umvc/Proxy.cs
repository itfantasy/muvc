using UnityEngine;
using System;
using System.Collections;

namespace itfantasy.umvc
{
    public class Proxy<T> : IBaseProxy where T : IBaseProxy, new()
    {
        private static T _i;

        public static T i
        {
            get
            {
                if (_i == null)
                {
                    _i = new T();
                    Facade.RegisterProxy(_i);
                }
                return _i;
            }
        }

        public void SendNotice(int cmdIndex, int noticeType, params object[] body)
        {
            Facade.SendNotice(cmdIndex, noticeType, body);
        }

        protected void SendAsyncNotice(int cmdIndex, int noticeType, Action<object> callback, object token, params object[] body)
        {
            Facade.SendAsyncNotice(cmdIndex, noticeType, callback, token, body);
        }

        protected void BroadNotice(int noticeType, object[] body)
        {
            Facade.BroadNotice(noticeType, body);
        }

        public virtual void Dispose()
        {
            _i = default(T);
        }
    }

}
