using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace itfantasy.umvc
{
    public interface INotice
    {
        int GetType();
        object[] GetBody();
        T GetBody<T>();
        void Finish();
        void SetCallback(Action<object> callback, object token);
        object GetToken();
    }
}

