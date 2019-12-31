using UnityEngine;
using System;
using System.Collections;
using itfantasy.lmjson;

namespace itfantasy.umvc
{
    public class Model : IDisposable
    {
        public object tag;

        public virtual void Initialize(JsonData json) { }

        public virtual void Dispose()
        {
            
        }
    }
}
