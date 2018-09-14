using UnityEngine;
using System;
using System.Collections;

namespace itfantasy.umvc
{
    public class Mediator : MonoBehaviour, IDisposable
    {
        bool inited = false;
        protected Command _command = null;

        public object token { get; set; }

        public void SignCommand(Command command)
        {
            this._command = command;
        }

        void Awake()
        {
            OnInitialize();
            inited = true;
        }

        // Use this for initialization
        void Start()
        {
            SetEventListener();
            OnShowing();
        }

        void OnEnable()
        {
            if (inited)
            {
                OnShowing();
            }
        }

        void OnDisable()
        {
            OnClosing();
        }

        void OnDestroy()
        {
            OnDispose();
        }

        protected virtual void OnInitialize() {
            
        }

        protected virtual void OnShowing() {
            UpdateViewContent();
        }

        protected virtual void OnClosing() { }

        protected virtual void OnDispose() { }

        protected T AttachView<T>() where T : View
        {
            return this.gameObject.AddComponent<T>();
        }

        public virtual void HandleNotice(INotice notice) { }

        protected virtual void OnClick(GameObject go) { }

        protected virtual void SetEventListener() { }

        protected virtual void UpdateViewContent() { }

        public virtual void Dispose() 
        {
            Destroy(this.gameObject);
        }
        
    }
}
