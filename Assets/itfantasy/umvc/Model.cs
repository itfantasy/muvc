using UnityEngine;
using System.Collections;
using itfantasy.lmjson;

namespace itfantasy.umvc
{
    public class Model
    {
        public object tag;

        public virtual void Initialize(JsonData json) { }

    }
}
