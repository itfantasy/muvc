using UnityEngine;
using System;
using System.Collections;

namespace itfantasy.umvc
{
    public class Proxy<T> : IBaseProxy where T : IBaseProxy, new()
    {
        private static T _ins;

        public static T ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new T();
                    Facade.RegisterProxy(_ins);
                }
                return _ins;
            }
        }



        public void SendNotice(int cmdIndex, int noticeType, params object[] body)
        {
            Facade.SendNotice(cmdIndex, noticeType, body);
        }

        protected void SendAsyncNotice(int cmdIndex, int noticeType, Action<INotice> callback, object token, params object[] body)
        {
            Facade.SendAsyncNotice(cmdIndex, noticeType, callback, token, body);
        }

        protected void BroadNotice(int noticeType, object[] body)
        {
            Facade.BroadNotice(noticeType, body);
        }

        public virtual void Dispose()
        {
            _ins = default(T);
        }
    }

}
