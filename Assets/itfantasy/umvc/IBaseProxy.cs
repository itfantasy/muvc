using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace itfantasy.umvc
{
    public interface IBaseProxy : IDisposable
    {
        void SendNotice(int index, int code, object value);
        void Dispose();
    }
}
