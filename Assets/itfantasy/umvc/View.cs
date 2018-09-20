using UnityEngine;
using System;
using System.Collections;

namespace itfantasy.umvc
{
    public class View : MonoBehaviour, IDisposable
    {
        public object token { get; set; }

        void Awake()
        {
            this.OnInitialize();
        }

        protected virtual void OnInitialize()
        {
        }

        public virtual void Dispose()
        {

        }
    }
}
