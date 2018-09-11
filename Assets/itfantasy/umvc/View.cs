using UnityEngine;
using System.Collections;

namespace itfantasy.umvc
{
    public class View : MonoBehaviour
    {
        public object token { get; set; }

        void Awake()
        {
            this.OnInitialize();
        }

        protected virtual void OnInitialize()
        {
        }
    }
}
