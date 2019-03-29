using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace itfantasy.igui
{
    public class UIClickListener : MonoBehaviour, IPointerClickHandler
    {
        public Action<GameObject> onClick;

        public static UIClickListener Get(GameObject go)
        {
            UIClickListener listener = go.GetComponent<UIClickListener>();
            if (listener == null) listener = go.AddComponent<UIClickListener>();
            return listener;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (onClick != null) onClick(gameObject);
        }

        void OnClick() { if (onClick != null) onClick(gameObject); }
        
    }
}
