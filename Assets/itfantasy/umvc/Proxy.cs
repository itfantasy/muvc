using UnityEngine;
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

        public void SendNotice(int index, int code, object value)
        {
            Notice notice = new Notice(code, value, this);
            Facade.SendNotice(index, notice);
        }

        public void Dispose()
        {
            _i = default(T);
        }
    }

}
